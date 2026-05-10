// Mentorship.API/Controllers/HealthController.cs
using Mentorship.API.Services;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class HealthController : ControllerBase
{
    private readonly IIdempotencyService _idempotencyService;
    private readonly IConnectionMultiplexer _redis;
    
    public HealthController(IIdempotencyService idempotencyService, IConnectionMultiplexer redis)
    {
        _idempotencyService = idempotencyService;
        _redis = redis;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var health = new
        {
            Status = "Healthy",
            Timestamp = DateTime.UtcNow,
            Components = new
            {
                Redis = new
                {
                    Status = await _idempotencyService.HealthCheckAsync() ? "Connected" : "Disconnected",
                    EndPoint = _redis.GetEndPoints().FirstOrDefault()?.ToString()
                },
                Api = new { Status = "Running" }
            }
        };
        
        return Ok(health);
    }
}