namespace Common.Core.Abstractions.Entities;

/// <summary>
///     Represents an entity with user tracking capabilities.
/// </summary>
public interface IUserTracking
{
    /// <summary>
    ///     Gets or sets the identifier of the user who created the entity.
    /// </summary>
    Guid CreatedBy { get; set; }

    /// <summary>
    ///     Gets or sets the identifier of the user who modified the entity.
    /// </summary>
    Guid? ModifiedBy { get; set; }
}