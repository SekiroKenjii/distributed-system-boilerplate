using Common.Core.Abstractions.System;

namespace Common.Infrastructure.Services.System;

public class DateTimeService : IDateTimeService
{
    public T DateTimeNow<T>(bool utc = true) where T : struct
    {
        return typeof(T).Name switch {
            "DateTime" => (T)(object)(utc ? DateTime.UtcNow : DateTime.Now),
            "DateTimeOffset" => (T)(object)(utc ? DateTimeOffset.UtcNow : DateTimeOffset.Now),
            _ => throw new InvalidOperationException("Type must be DateTime or DateTimeOffset.")
        };
    }

    /// <inheritdoc />
    public DateOnly DateOnlyNow => DateOnly.FromDateTime(DateTimeNow<DateTime>());
}