using System;
using System.Text.Json;
using StackExchange.Redis;

namespace Mentorship.API.Services;

public interface IIdempotencyService
{
    Task<bool>RequestExistAsync(string key, string operationType);
    Task StoreResponseAsync(string key, string operationType, object response, int statuscode, int ttlSec = 86400);
    Task<T?> GetResponseAsync<T>(string key, string operationType);
    Task<(int StatusCode, T? Response)> GetCachedResponseAsync<T>(string key, string operationType);
}

public class RedisIdempotencyService : IIdempotencyService
{
       private readonly IConnectionMultiplexer _redis;
    private readonly IDatabase _db;
    
    public RedisIdempotencyService(IConnectionMultiplexer redis)
    {
        _redis = redis;
        _db = redis.GetDatabase();
    }

    private static string GetKey(string key, string operationType) => $"idempotency:{operationType}:{key}";

    public async Task<bool> RequestExistAsync(string key, string operationType)
    {
        var redisKey = GetKey(key, operationType);
        return await _db.KeyExistsAsync(redisKey);
    }
    public async Task StoreResponseAsync(string key, string operationType, object response, int statuscode, int ttlSec = 86400)
    {
        var redisKey = GetKey(key, operationType);
        var cacheEntry = new IdempotencyCacheEntry
        {
          StatusCode = statuscode,
          Response = response,
          StoredAt = DateTime.UtcNow  
        };
        var json = JsonSerializer.Serialize(cacheEntry);

        await _db.StringSetAsync(redisKey, json, TimeSpan.FromSeconds(ttlSec));
    }
    public async Task<T?> GetResponseAsync<T>(string key, string operationType)
    {
        var redisKey = GetKey(key, operationType);
        var json = await _db.StringGetAsync(redisKey);

        if(json.IsNullOrEmpty) return default;

        var entry = JsonSerializer.Deserialize<IdempotencyCacheEntry>(json!);
        return JsonSerializer.Deserialize<T>(entry!.Response.ToString()!);
    }
      public async Task<(int StatusCode, T? Response)> GetCachedResponseAsync<T>(string key, string operationType)
    {
        var redisKey = GetKey(key, operationType);
        var json = await _db.StringGetAsync(redisKey);
        
        if (json.IsNullOrEmpty)
            return (0, default);
            
        var entry = JsonSerializer.Deserialize<IdempotencyCacheEntry>(json!);
        var response = JsonSerializer.Deserialize<T>(entry!.Response.ToString()!);
        
        return (entry.StatusCode, response);
    }

    private class IdempotencyCacheEntry
    {
        public int StatusCode {get; set;}
        public required object Response {get; set;}
        public DateTime StoredAt {get; set;}
    }

    
}
