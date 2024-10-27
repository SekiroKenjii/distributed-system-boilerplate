namespace Common.Core.Abstractions.Cache;

/// <summary>
///     Defines methods for publishing and subscribing to cache invalidation events.
/// </summary>
public interface ICacheInvalidationService
{
    /// <summary>
    ///     Publishes a cache invalidation event for the specified key.
    /// </summary>
    /// <param name="key">The key for which the cache invalidation event is published.</param>
    /// <param name="prefixChannel">Indicates whether to prefix the channel with a specific value.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task PublishCacheInvalidationEventAsync(string key, bool prefixChannel = false);

    /// <summary>
    ///     Subscribes to cache invalidation events.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task SubscribeCacheInvalidationEvent();
}