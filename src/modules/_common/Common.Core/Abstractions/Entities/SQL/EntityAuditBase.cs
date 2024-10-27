namespace Common.Core.Abstractions.Entities.SQL;

/// <summary>
///     Represents an auditable entity with a primary key.
/// </summary>
/// <typeparam name="TKey">The type of the primary key.</typeparam>
public interface IEntityAuditBase<TKey> : IEntity<TKey>, IAuditable { }

/// <summary>
///     Represents the base class for auditable entities with a primary key.
/// </summary>
/// <typeparam name="TKey">The type of the primary key.</typeparam>
public abstract class EntityAuditBase<TKey> : EntityBase<TKey>, IEntityAuditBase<TKey>
{
    /// <inheritdoc />
    public DateTimeOffset CreatedAt { get; set; }

    /// <inheritdoc />
    public DateTimeOffset? ModifiedAt { get; set; }

    /// <inheritdoc />
    public Guid CreatedBy { get; set; }

    /// <inheritdoc />
    public Guid? ModifiedBy { get; set; }

    /// <inheritdoc />
    public bool IsDeleted { get; set; }

    /// <inheritdoc />
    public DateTimeOffset? DeletedAt { get; set; }

    /// <inheritdoc />
    public void Undo()
    {
        IsDeleted = false;
        DeletedAt = null;
    }
}