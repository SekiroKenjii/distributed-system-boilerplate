using Common.Core.Abstractions.System;

namespace Common.Core.Abstractions.Email;

/// <summary>
///     Defines the contract for an email service that can send emails asynchronously.
/// </summary>
public interface IEmailService : IScopedService
{
    /// <summary>
    ///     Sends an email asynchronously.
    /// </summary>
    /// <param name="to">The recipient's email address.</param>
    /// <param name="subject">The subject of the email.</param>
    /// <param name="body">The body content of the email.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task SendEmailAsync(string to, string subject, string body);
}