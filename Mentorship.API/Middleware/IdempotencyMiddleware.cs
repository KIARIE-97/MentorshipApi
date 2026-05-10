using System;
using System.Text.Json;
using Mentorship.API.Attributes;
using Mentorship.API.Services;

namespace Mentorship.API.Middleware;

public class IdempotencyMiddleware (RequestDelegate next, IIdempotencyService idempotencyService, ILogger<IdempotencyMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly IIdempotencyService _idempotencyService = idempotencyService;
    private readonly ILogger<IdempotencyMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
         _logger.LogWarning("🔵 MIDDLEWARE INVOKED");
        //handle post ,put, patch
        if(context.Request.Method == HttpMethods.Post ||
            context.Request.Method == HttpMethods.Put ||
            context.Request.Method == HttpMethods.Patch
        )
        {
            var idempotencyKey = context.Request.Headers["idempotency-Key"].ToString();
             if (string.IsNullOrEmpty(idempotencyKey))
        {
            idempotencyKey = $"auto-generated-{DateTime.UtcNow:yyyyMMdd-HHmmss}-{Guid.NewGuid()}";
            _logger.LogWarning("⚠️ No Idempotency-Key header, auto-generated: {Key}", idempotencyKey);}

            if(!string.IsNullOrEmpty(idempotencyKey))
            {
                //get operation from endpoint
                var endpoint = context.GetEndpoint();
                var attribute = endpoint?.Metadata.GetMetadata<IdempotentAttribute>();

                if( attribute != null)
                {
                    var operationType = attribute.OperationType;
                    var cachekey = $"{operationType}:{idempotencyKey}";
                    //check if processed

                    var (statusCode, cachedResponse) = await _idempotencyService.GetCachedResponseAsync<object>(idempotencyKey, operationType);
                    if(cachedResponse != null)
                    {
                        _logger.LogInformation("Idempotent request detected: {Key} for {Operation}", idempotencyKey, operationType);
                        context.Response.StatusCode = statusCode;
                        await context.Response.WriteAsJsonAsync(cachedResponse);
                        return;
                    }
                    //else lets store the original response body stream
                    var originalBodyStream = context.Response.Body;
                    using var memoryStream = new MemoryStream();
                    context.Response.Body = memoryStream;

                    //process the request
                    await _next(context);

                    //cache the response
                    memoryStream.Position = 0;
                    var responseBody = await new StreamReader(memoryStream).ReadToEndAsync();
                    var responseObject = JsonSerializer.Deserialize<object>(responseBody);

                    await _idempotencyService.StoreResponseAsync(
                        idempotencyKey,
                        operationType,
                        responseObject!,
                        context.Response.StatusCode,
                        attribute.TtlSeconds 
                    );

                    //copy message to original stream
                    memoryStream.Position = 0;
                    await memoryStream.CopyToAsync(originalBodyStream);
                    context.Response.Body = originalBodyStream;
                    return;
                }
            }
        }

        await _next(context);
    }



}
