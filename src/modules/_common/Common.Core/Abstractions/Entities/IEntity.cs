namespace Common.Core.Abstractions.Entities;

/// <summary>
///     Represents an entity with a primary key.
/// </summary>
/// <typeparam name="TKey">The type of the primary key.</typeparam>
public interface IEntity<TKey>
{
    /// <summary>
    ///     Gets or sets the primary key.
    /// </summary>
    TKey Id { get; set; }
}