namespace Common.Core.Abstractions.Entities;

/// <summary>
///     Represents an entity with soft delete capabilities.
/// </summary>
public interface ISoftDelete
{
    /// <summary>
    ///     Gets or sets a value indicating whether the entity is deleted.
    /// </summary>
    bool IsDeleted { get; set; }

    /// <summary>
    ///     Gets or sets the deletion date and time.
    /// </summary>
    DateTimeOffset? DeletedAt { get; set; }

    /// <summary>
    ///     Reverts the deletion of the entity.
    /// </summary>
    void Undo();
}