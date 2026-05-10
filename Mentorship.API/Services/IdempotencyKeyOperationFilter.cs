using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Mentorship.API.Services;

public class IdempotencyKeyOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // Check if it's a POST, PUT, or PATCH method
        var isPostOrPut = context.MethodInfo.GetCustomAttributes(true)
            .Any(attr => attr is HttpPostAttribute || 
                         attr is HttpPutAttribute || 
                         attr is HttpPatchAttribute);
        
        if (isPostOrPut)
        {
            operation.Parameters ??= new List<OpenApiParameter>();
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Idempotency-Key",
                In = ParameterLocation.Header,
                Required = false,  // Not required but recommended
                Schema = new OpenApiSchema { Type = "string" },
                Description = "Unique key to ensure idempotency. Use UUID format."
            });
        }
    }
}

