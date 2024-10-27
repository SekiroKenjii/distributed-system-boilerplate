namespace Common.Core.Abstractions.Entities;

/// <summary>
///     Represents an entity with date tracking capabilities.
/// </summary>
public interface IDateTracking
{
    /// <summary>
    ///     Gets or sets the creation date and time.
    /// </summary>
    DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    ///     Gets or sets the modification date and time.
    /// </summary>
    DateTimeOffset? ModifiedAt { get; set; }
}