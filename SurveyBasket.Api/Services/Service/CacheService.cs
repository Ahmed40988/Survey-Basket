using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace SurveyBasket.Api.Services.Service
{
    public class CacheService(IDistributedCache distributedCache, ILogger<CacheService> logger) : ICacheService
    {
        private readonly IDistributedCache _distributedCache = distributedCache;
        private readonly ILogger<CacheService> _logger = logger;

        public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
        {
            _logger.LogInformation("Get cache with key: {key}", key);
            var CachValue = await _distributedCache.GetStringAsync(key, cancellationToken);

            return CachValue is null
                ? null
                : JsonSerializer.Deserialize<T>(CachValue);
        }

        public async Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default) where T : class
        {
            _logger.LogInformation("Set cache with key: {key}", key);
            await _distributedCache.SetStringAsync(key, JsonSerializer.Serialize(value), cancellationToken);
        }
        public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Remove cache with key: {key}", key);
            await _distributedCache.RemoveAsync(key, cancellationToken);

        }

    }
}
