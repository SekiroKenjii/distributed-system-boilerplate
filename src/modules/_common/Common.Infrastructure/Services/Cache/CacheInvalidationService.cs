using Common.Core.Abstractions.Cache;
using StackExchange.Redis;

namespace Common.Infrastructure.Services.Cache;

public class CacheInvalidationService(
    ICacheService cacheService,
    ISubscriber redisSubscription) : ICacheInvalidationService
{
    private static readonly RedisChannel CacheChannel = RedisChannel.Pattern("invalidate-cache");

    private static readonly RedisChannel PrefixCacheChannel = RedisChannel.Pattern("invalidate-prefix-cache");

    /// <inheritdoc />
    public async Task PublishCacheInvalidationEventAsync(string key, bool prefixChannel = false)
    {
        var channel = prefixChannel ? PrefixCacheChannel : CacheChannel;

        await redisSubscription.PublishAsync(channel, key);
    }

    /// <inheritdoc />
    public async Task SubscribeCacheInvalidationEvent()
    {
        await redisSubscription.SubscribeAsync(
            CacheChannel,
            (channel, message) => { _ = cacheService.RemoveAsync(message.ToString()); }
        );

        await redisSubscription.SubscribeAsync(
            PrefixCacheChannel,
            (channel, message) => { _ = cacheService.RemoveByPrefixAsync(message.ToString()); }
        );
    }
}