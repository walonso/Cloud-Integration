using StackExchange.Redis;

namespace Services.Integration.Infrastructure.Azure;
using Services.Integration.Application.Interfaces;

public class RedisIntegration : ICacheDatabase
{

    private readonly IConnectionMultiplexer _redis;

    public RedisIntegration(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }

    public async Task<string> GetValueAsync(string key)
    {
        var db = _redis.GetDatabase();
        var value = await db.StringGetAsync(key);
        return value.ToString();
    }

    public async Task<bool> SetValueAsync(string key, string value)
    {
        var db = _redis.GetDatabase();
        var response = await db.StringSetAsync(key,value);
        return response;
    }
}