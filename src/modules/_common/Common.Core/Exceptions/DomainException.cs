namespace Common.Core.Exceptions;

/// <summary>
///     Represents errors that occur during application execution in the domain layer.
/// </summary>
public class DomainException : Exception
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DomainException" /> class with a specified title and error message.
    /// </summary>
    /// <param name="title">The title of the exception.</param>
    /// <param name="message">The message that describes the error.</param>
    protected DomainException(string title, string message) : base(message)
    {
        Title = title;
    }

    /// <summary>
    ///     Gets the title of the exception.
    /// </summary>
    public string Title { get; }

    /// <summary>
    ///     Creates a new instance of the <see cref="DomainException" /> class representing a fatal error.
    /// </summary>
    /// <param name="message">The message that describes the fatal error.</param>
    /// <returns>A new instance of the <see cref="DomainException" /> class.</returns>
    public static DomainException FatalError(string message)
    {
        return new DomainException("Internal Server Error", message);
    }
}