using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using SurveyBasket.Domain.Options.EmailSettingOptions;

namespace SurveyBasket.Infrastructure.Services.Email.ConfirmationEmail
{
    public class ConfirmEmailNotificationService(
        IOptions<EmailSetting> mailOptions,
        ILogger<ConfirmEmailNotificationService> logger) : INotificationService
    {
        private readonly EmailSetting _mailoptions = mailOptions.Value;
        private readonly ILogger<ConfirmEmailNotificationService> _logger = logger;

        public async Task NotifyAsync(string message, string subject, string toEmail)
        {
            var msg = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_mailoptions.Mail),
                Subject = subject
            };

            msg.To.Add(MailboxAddress.Parse(toEmail));

            var builder = new BodyBuilder { HtmlBody = message };

            msg.Body = builder.ToMessageBody();

            using var client = new SmtpClient();

            await client.ConnectAsync(
                _mailoptions.Host,
                _mailoptions.Port,
                SecureSocketOptions.StartTls);

            await client.AuthenticateAsync(
                _mailoptions.Mail,
                _mailoptions.Password);

            var result = await client.SendAsync(msg);
            _logger.LogInformation("The server accepted the message: {0}", result.ResponseText);

            await client.DisconnectAsync(true);
        }
    }
}