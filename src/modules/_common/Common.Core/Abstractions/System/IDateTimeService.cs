namespace Common.Core.Abstractions.System;

/// <summary>
///     Provides date and time services.
/// </summary>
public interface IDateTimeService
{
    /// <summary>
    ///     Gets the current date.
    /// </summary>
    DateOnly DateOnlyNow { get; }

    /// <summary>
    ///     Gets the current date and time.
    /// </summary>
    /// <typeparam name="T">The type of the date and time.</typeparam>
    /// <param name="utc">If set to <c>true</c>, returns the date and time in UTC.</param>
    /// <returns>The current date and time.</returns>
    T DateTimeNow<T>(bool utc = true) where T : struct;
}