namespace Common.Core.Pagination;

/// <summary>
///     Represents a paginated list of items.
/// </summary>
/// <typeparam name="T">The type of items in the paginated list.</typeparam>
public class PagedList<T>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="PagedList{T}" /> class.
    /// </summary>
    /// <param name="items">The items in the current page.</param>
    /// <param name="totalItems">The total number of items.</param>
    /// <param name="pageNumber">The current page number.</param>
    /// <param name="pageSize">The size of the page.</param>
    public PagedList(IEnumerable<T> items, int totalItems, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalItems = totalItems;
        if (totalItems > 0) TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        Data = items as IList<T> ?? new List<T>(items);
    }

    /// <summary>
    ///     Gets the data in the current page.
    /// </summary>
    public IList<T> Data { get; }

    /// <summary>
    ///     Gets the current page number.
    /// </summary>
    public int PageNumber { get; }

    /// <summary>
    ///     Gets the size of the page.
    /// </summary>
    public int PageSize { get; }

    /// <summary>
    ///     Gets the total number of pages.
    /// </summary>
    public int TotalPages { get; }

    /// <summary>
    ///     Gets the total number of items.
    /// </summary>
    public int TotalItems { get; }

    /// <summary>
    ///     Gets a value indicating whether the current page is the first page.
    /// </summary>
    public bool IsFirstPage => PageNumber == 1;

    /// <summary>
    ///     Gets a value indicating whether the current page is the last page.
    /// </summary>
    public bool IsLastPage => PageNumber == TotalPages && TotalPages > 0;
}