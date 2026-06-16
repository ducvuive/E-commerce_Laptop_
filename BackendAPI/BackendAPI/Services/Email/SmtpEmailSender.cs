using System.Net;
using System.Net.Mail;
using System.Text;
using Microsoft.Extensions.Options;

namespace BackendAPI.Services.Email;

public sealed class SmtpEmailSender : IEmailSender
{
    private readonly SmtpEmailOptions options;

    public SmtpEmailSender(IOptions<SmtpEmailOptions> options)
    {
        this.options = options.Value;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string htmlBody, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(options.Host))
        {
            throw new InvalidOperationException("SMTP host is not configured.");
        }

        using var message = new MailMessage
        {
            From = new MailAddress(options.From, options.FromName),
            Subject = subject,
            Body = htmlBody,
            IsBodyHtml = true,
            BodyEncoding = Encoding.UTF8,
            SubjectEncoding = Encoding.UTF8
        };
        message.To.Add(toEmail);

        using var smtpClient = new SmtpClient(options.Host, options.Port)
        {
            EnableSsl = options.EnableSsl
        };

        if (!string.IsNullOrWhiteSpace(options.UserName))
        {
            smtpClient.Credentials = new NetworkCredential(options.UserName, options.Password);
        }

        await smtpClient.SendMailAsync(message, cancellationToken);
    }
}
