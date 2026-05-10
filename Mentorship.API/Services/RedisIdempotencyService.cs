// Mentorship.API/Services/RedisIdempotencyService.cs
using StackExchange.Redis;
using System.Text.Json;

namespace Mentorship.API.Services;

public interface IIdempotencyService
{
    Task<bool> RequestExistsAsync(string key, string operationType);
    Task StoreResponseAsync(string key, string operationType, object response, int statusCode, int ttlSeconds = 86400);
    Task<T?> GetResponseAsync<T>(string key, string operationType);
    Task<(int StatusCode, T? Response)> GetCachedResponseAsync<T>(string key, string operationType);
    Task<bool> HealthCheckAsync();
}

public class RedisIdempotencyService : IIdempotencyService
{
    private readonly IConnectionMultiplexer _redis;
    private readonly IDatabase _db;
    private readonly ILogger<RedisIdempotencyService> _logger;
    private readonly string _instanceName;
    
    public RedisIdempotencyService(
        IConnectionMultiplexer redis, 
        IConfiguration config,
        ILogger<RedisIdempotencyService> logger)
    {
        _redis = redis;
        _db = redis.GetDatabase();
        _logger = logger;
        _instanceName = config["Redis:InstanceName"] ?? "MentorshipAPI:";
    }
    
    private string GetKey(string key, string operationType) => $"{_instanceName}{operationType}:{key}";
    
    public async Task<bool> RequestExistsAsync(string key, string operationType)
    {
        try
        {
            var redisKey = GetKey(key, operationType);
            return await _db.KeyExistsAsync(redisKey);
        }
        catch (RedisConnectionException ex)
        {
            _logger.LogError(ex, "Redis connection error when checking idempotency key {Key}", key);
            // Fail open - allow the request to proceed
            return false;
        }
    }
    
    public async Task StoreResponseAsync(string key, string operationType, object response, int statusCode, int ttlSeconds = 86400)
    {
        try
        {
               if (ttlSeconds <= 0)
        {
            _logger.LogWarning("⚠️ Invalid TTL {Ttl}s - using default 86400 seconds (24 hours)", ttlSeconds);
            ttlSeconds = 86400;  // Default to 24 hours
        }
            var redisKey = GetKey(key, operationType);
            
            var cacheEntry = new IdempotencyCacheEntry
            {
                StatusCode = statusCode,
                Response = response,
                StoredAt = DateTime.UtcNow
            };
            
            var json = JsonSerializer.Serialize(cacheEntry);
            
            _logger.LogInformation("STORING TO REDIS: Key={RedisKey}, TTL={Ttl}s, Data={Json}", 
                redisKey, ttlSeconds, json.Length > 100 ? json[..100] + "..." : json);
            
            // Store with automatic expiry
            var success = await _db.StringSetAsync(redisKey, json, TimeSpan.FromSeconds(ttlSeconds));
            
            if (success)
            {
                _logger.LogInformation("✅ SUCCESSFULLY stored to Redis: {RedisKey}", redisKey);
                
                // Verify it was stored
                var exists = await _db.KeyExistsAsync(redisKey);
                _logger.LogInformation("Verification - Key exists: {Exists}", exists);
            }
            else
            {
                _logger.LogError("❌ FAILED to store to Redis: {RedisKey}", redisKey);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ EXCEPTION storing to Redis for key {Key}", key);
        }
    }
    
    
    public async Task<T?> GetResponseAsync<T>(string key, string operationType)
    {
        try
        {
            var redisKey = GetKey(key, operationType);
            var json = await _db.StringGetAsync(redisKey);
            
            if (json.IsNullOrEmpty)
                return default;
                
            var entry = JsonSerializer.Deserialize<IdempotencyCacheEntry>(json!);
            if (entry?.Response == null)
                return default;
                
            return JsonSerializer.Deserialize<T>(entry.Response.ToString()!);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve cached response for {Key}", key);
            return default;
        }
    }
    
    public async Task<(int StatusCode, T? Response)> GetCachedResponseAsync<T>(string key, string operationType)
    {
        try
        {
            var redisKey = GetKey(key, operationType);
            _logger.LogInformation("READING FROM REDIS: Key={RedisKey}", redisKey);
            
            var json = await _db.StringGetAsync(redisKey);
            
            if (json.IsNullOrEmpty)
            {
                _logger.LogInformation("❌ Key NOT FOUND in Redis: {RedisKey}", redisKey);
                return (0, default);
            }
            
            _logger.LogInformation("✅ Key FOUND in Redis: {RedisKey}, Data={Json}", redisKey, json.ToString());
            
            var entry = JsonSerializer.Deserialize<IdempotencyCacheEntry>(json!);
            if (entry?.Response == null)
                return (0, default);
                
            var response = JsonSerializer.Deserialize<T>(entry.Response.ToString()!);
            return (entry.StatusCode, response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ EXCEPTION reading from Redis for key {Key}", key);
            return (0, default);
        }
    }
    
    public async Task<bool> HealthCheckAsync()
    {
        try
        {
            var ping = await _db.PingAsync();
            return ping.TotalMilliseconds > 0;
        }
        catch
        {
            return false;
        }
    }
    
    private class IdempotencyCacheEntry
    {
        public int StatusCode { get; set; }
        public object Response { get; set; } = null!;
        public DateTime StoredAt { get; set; }
    }
}