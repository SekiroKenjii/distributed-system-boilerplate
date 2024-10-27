using Common.Core.Abstractions.System;
using MailKit.Security;
using MimeKit;

namespace Common.Core.Abstractions.Email;

/// <summary>
///     Defines an interface for an SMTP client wrapper that provides methods for connecting,
///     authenticating, sending emails, and disconnecting from an SMTP server.
/// </summary>
public interface ISmtpClientService : IScopedService, IDisposable
{
    /// <summary>
    ///     Asynchronously connects to the SMTP server.
    /// </summary>
    /// <param name="host">The host name of the SMTP server.</param>
    /// <param name="port">The port number to connect to.</param>
    /// <param name="options">The secure socket options to use.</param>
    /// <returns>A task that represents the asynchronous connect operation.</returns>
    Task ConnectAsync(string host, int port, SecureSocketOptions options);

    /// <summary>
    ///     Asynchronously authenticates with the SMTP server using the specified credentials.
    /// </summary>
    /// <param name="username">The username to authenticate with.</param>
    /// <param name="password">The password to authenticate with.</param>
    /// <returns>A task that represents the asynchronous authenticate operation.</returns>
    Task AuthenticateAsync(string username, string password);

    /// <summary>
    ///     Asynchronously sends the specified email message.
    /// </summary>
    /// <param name="message">The email message to send.</param>
    /// <returns>A task that represents the asynchronous send operation.</returns>
    Task SendAsync(MimeMessage message);

    /// <summary>
    ///     Asynchronously disconnects from the SMTP server.
    /// </summary>
    /// <param name="quit">If true, sends a QUIT command to the server before disconnecting.</param>
    /// <returns>A task that represents the asynchronous disconnect operation.</returns>
    Task DisconnectAsync(bool quit);
}