using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;

namespace McGurkin.Services
{
    public class EmailSenderConfig
    {
        public required From From { get; set; }
        public required string Host { get; set; }

        public static EmailSenderConfig FromRootConfiguration(IConfiguration configuration)
        {
            var returnValue = configuration.GetSection("EmailSenderConfig").Get<EmailSenderConfig>() ?? throw new Exception("Missing Email Sender Configuration section.");
            return returnValue;
        }
    }

    public class From
    {
        public required string Address { get; set; }
        public required string DisplayName { get; set; }
        public required string Password { get; set; }
    }

    public partial class EmailSender(IConfiguration config, ILogger<EmailSender> logger) : Microsoft.AspNetCore.Identity.UI.Services.IEmailSender
    {
        private readonly EmailSenderConfig _config = EmailSenderConfig.FromRootConfiguration(config);
        private readonly ILogger<EmailSender> _logger = logger;

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MailMessage msg = new();
            msg.To.Add(new MailAddress(email));
            msg.From = new MailAddress(_config.From.Address, _config.From.DisplayName);
            msg.Subject = subject;
            msg.Body = htmlMessage;
            msg.IsBodyHtml = true;

            SmtpClient client = new()
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_config.From.Address, _config.From.Password),
                Port = 587,
                Host = _config.Host,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true
            };

            await client.SendMailAsync(msg);
            _logger?.LogInformation("Successfully sent email message.");
        }
    }
}
