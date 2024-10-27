namespace Common.Core.Abstractions.Entities;

/// <summary>
///     Represents an auditable entity with date tracking, user tracking, and soft delete capabilities.
/// </summary>
public interface IAuditable : IDateTracking, IUserTracking, ISoftDelete { }