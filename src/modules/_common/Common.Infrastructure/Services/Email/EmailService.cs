using Common.Core.Abstractions.Email;
using Common.Infrastructure.DependencyInjection.Options;
using MailKit.Security;
using MimeKit;

namespace Common.Infrastructure.Services.Email;

public class EmailService(
    IEmailConfiguration emailConfiguration,
    ISmtpClientService smtpClientService) : IEmailService
{
    /// <inheritdoc />
    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var emailMessage = new MimeMessage {
            Subject = subject,
            Body = new TextPart("html") {
                Text = body
            }
        };
        emailMessage.From.Add(new MailboxAddress(emailConfiguration.SenderName, emailConfiguration.SenderEmail));
        emailMessage.To.Add(new MailboxAddress("", to));

        await smtpClientService.ConnectAsync(
            emailConfiguration.SmtpServer,
            emailConfiguration.SmtpPort,
            SecureSocketOptions.StartTls
        );
        await smtpClientService.AuthenticateAsync(emailConfiguration.SmtpUsername, emailConfiguration.SmtpPassword);
        await smtpClientService.SendAsync(emailMessage);
        await smtpClientService.DisconnectAsync(true);
    }
}