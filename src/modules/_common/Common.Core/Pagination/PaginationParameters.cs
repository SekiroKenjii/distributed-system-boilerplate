namespace Common.Core.Pagination;

/// <summary>
///     Represents the base class for pagination parameters.
/// </summary>
public abstract class PaginationParameters
{
    /// <summary>
    ///     Gets the maximum allowed page size.
    /// </summary>
    internal virtual int MaxPageSize { get; } = 20;

    /// <summary>
    ///     Gets or sets the default page size.
    /// </summary>
    internal virtual int DefaultPageSize { get; set; } = 10;

    /// <summary>
    ///     Gets or sets the current page number.
    /// </summary>
    public virtual int PageNumber { get; set; } = 1;

    /// <summary>
    ///     Gets or sets the size of the page. If the value exceeds <see cref="MaxPageSize" />, it will be set to
    ///     <see cref="MaxPageSize" />.
    /// </summary>
    public int PageSize {
        get => DefaultPageSize;
        set => DefaultPageSize = value > MaxPageSize ? MaxPageSize : value;
    }
}