using Common.Core.Abstractions.System;

namespace Common.Infrastructure.DependencyInjection.Options;

public interface IEmailConfiguration : IOptionRoot
{
    string SenderName { get; set; }

    string SenderEmail { get; set; }

    string SmtpServer { get; set; }

    int SmtpPort { get; set; }

    string SmtpUsername { get; set; }

    string SmtpPassword { get; set; }

    bool UseSsl { get; set; }
}

public abstract class EmailConfiguration : IEmailConfiguration
{
    public string SenderName { get; set; } = string.Empty;

    public string SenderEmail { get; set; } = string.Empty;

    public string SmtpServer { get; set; } = string.Empty;

    public int SmtpPort { get; set; }

    public string SmtpUsername { get; set; } = string.Empty;

    public string SmtpPassword { get; set; } = string.Empty;

    public bool UseSsl { get; set; } = true;
}