namespace Common.Core.Extensions;

/// <summary>
///     Provides extension methods for <see cref="IEnumerable{T}" />.
/// </summary>
public static class EnumerableExtensions
{
    /// <summary>
    ///     Performs the specified action on each element of the <see cref="IEnumerable{T}" />.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the collection.</typeparam>
    /// <param name="collection">The collection of elements.</param>
    /// <param name="action">The action to perform on each element.</param>
    /// <exception cref="ArgumentNullException">Thrown when the action is null.</exception>
    public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
    {
        ArgumentNullException.ThrowIfNull(action);

        foreach (var item in collection) action(item);
    }
}