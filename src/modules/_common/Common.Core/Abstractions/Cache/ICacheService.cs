namespace Common.Core.Abstractions.Cache;

/// <summary>
///     Defines a contract for a cache service that supports asynchronous operations for getting, setting, and removing
///     cached items.
/// </summary>
public interface ICacheService
{
    /// <summary>
    ///     Asynchronously retrieves an item from the cache.
    /// </summary>
    /// <typeparam name="T">The type of the item to retrieve.</typeparam>
    /// <param name="key">The key of the item to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains the retrieved item, or null if the
    ///     item does not exist.
    /// </returns>
    Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
        where T : class;

    /// <summary>
    ///     Asynchronously sets an item in the cache.
    /// </summary>
    /// <typeparam name="T">The type of the item to set.</typeparam>
    /// <param name="key">The key of the item to set.</param>
    /// <param name="value">The value of the item to set.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default)
        where T : class;

    /// <summary>
    ///     Asynchronously removes an item from the cache.
    /// </summary>
    /// <param name="key">The key of the item to remove.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task RemoveAsync(string key, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Asynchronously removes items from the cache that match a specified prefix.
    /// </summary>
    /// <param name="prefixKey">The prefix of the keys of the items to remove.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task RemoveByPrefixAsync(string prefixKey, CancellationToken cancellationToken = default);
}