using System.Collections.Concurrent;
using Common.Core.Abstractions.Cache;
using Common.Core.Abstractions.Serializer;
using Microsoft.Extensions.Caching.Distributed;

namespace Common.Infrastructure.Services.Cache;

public class CacheService(
    IDistributedCache distributedCache,
    ISerializerService serializerService) : ICacheService
{
    private static readonly ConcurrentDictionary<string, bool> CacheKeys = new();

    /// <inheritdoc />
    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
    {
        var cacheValue = await distributedCache.GetStringAsync(key, cancellationToken);

        return cacheValue is null ? null : serializerService.Deserialize<T>(cacheValue);
    }

    /// <inheritdoc />
    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        await distributedCache.RemoveAsync(key, cancellationToken);

        CacheKeys.TryRemove(key, out var _);
    }

    /// <inheritdoc />
    public async Task RemoveByPrefixAsync(string prefixKey, CancellationToken cancellationToken = default)
    {
        var tasks = CacheKeys.Keys
                             .Where(k => k.StartsWith(prefixKey))
                             .Select(k => RemoveAsync(k, cancellationToken));

        await Task.WhenAll(tasks);
    }

    /// <inheritdoc />
    public async Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default) where T : class
    {
        var cacheValue = serializerService.Serialize(value);

        await distributedCache.SetStringAsync(key, cacheValue, cancellationToken);

        CacheKeys.TryAdd(key, false);
    }
}