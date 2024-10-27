using Common.Core.Abstractions.Email;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Common.Infrastructure.Services.Email;

public sealed class SmtpClientService : ISmtpClientService
{
    private readonly SmtpClient _smtpClient = new();

    private bool _disposed;

    public Task ConnectAsync(string host, int port, SecureSocketOptions options)
    {
        return _smtpClient.ConnectAsync(host, port, options);
    }

    public Task AuthenticateAsync(string username, string password)
    {
        return _smtpClient.AuthenticateAsync(username, password);
    }

    public Task SendAsync(MimeMessage message)
    {
        return _smtpClient.SendAsync(message);
    }

    public Task DisconnectAsync(bool quit)
    {
        return _smtpClient.DisconnectAsync(quit);
    }

    public void Dispose()
    {
        Dispose(true);
        // ReSharper disable once GCSuppressFinalizeForTypeWithoutDestructor
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (!_disposed && disposing) _smtpClient.Dispose();

        _disposed = true;
    }
}