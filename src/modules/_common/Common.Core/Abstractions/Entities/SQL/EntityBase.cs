namespace Common.Core.Abstractions.Entities.SQL;

/// <summary>
///     Represents the base class for entities with a primary key.
/// </summary>
/// <typeparam name="TKey">The type of the primary key.</typeparam>
public abstract class EntityBase<TKey> : IEntity<TKey>
{
    public required TKey Id { get; set; }
}