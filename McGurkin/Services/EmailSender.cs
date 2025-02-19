using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;

namespace McGurkin.Services
{
    public interface IEmailSenderConfig
    {
        From From { get; set; }
        string Host { get; set; }
    }

    public class EmailSenderConfig : IEmailSenderConfig
    {
        public required From From { get; set; }
        public required string Host { get; set; }

        public static EmailSenderConfig GetEmpty()
        {
            var returnValue = new EmailSenderConfig
            {
                From = new From
                {
                    Address = "",
                    DisplayName = "",
                    Password = ""
                },
                Host = ""
            };
            return returnValue;
        }
    }

    public class From
    {
        public required string Address { get; set; }
        public required string DisplayName { get; set; }
        public required string Password { get; set; }
    }

    public partial class EmailSender : Microsoft.AspNetCore.Identity.UI.Services.IEmailSender
    {
        private readonly EmailSenderConfig _emailSenderConfig;
        private readonly ILogger<EmailSender> _logger;

        public EmailSender(IConfiguration config, ILogger<EmailSender> logger)
        {
            _emailSenderConfig = EmailSenderConfig.GetEmpty();
            config.GetRequiredSection("EmailSenderConfig").Bind(_emailSenderConfig);

            _logger = logger;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MailMessage msg = new();
            msg.To.Add(new MailAddress(email));
            msg.From = new MailAddress(_emailSenderConfig.From.Address, _emailSenderConfig.From.DisplayName);
            msg.Subject = subject;
            msg.Body = htmlMessage;
            msg.IsBodyHtml = true;

            SmtpClient client = new()
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_emailSenderConfig.From.Address, _emailSenderConfig.From.Password),
                Port = 587,
                Host = _emailSenderConfig.Host,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true,
            };

            await client.SendMailAsync(msg);
            _logger?.LogInformation("Successfully sent email message.");
        }
    }
}
