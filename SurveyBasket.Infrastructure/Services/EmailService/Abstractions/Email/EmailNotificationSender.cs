using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using SurveyBasket.Application.Interfaces.NotificationService.Abstractions;
using SurveyBasket.Domain.Options.EmailSettingOptions;
using SurveyBasket.Infrastructure.Services.NotificationService.Abstractions;

namespace SurveyBasket.Infrastructure.Services.EmailService.Abstractions.Email
{
    public class EmailNotificationSender(
    IOptions<EmailSettings> emailSettingOptions) : INotificationSender
    {
        private readonly EmailSettings _settings = emailSettingOptions.Value;

        public async Task<bool> SendAsync(string sendTo, string message, string subject)
        {
            var msg = new MimeMessage
            {
                Subject = subject
            };

            msg.From.Add(
                MailboxAddress.Parse(_settings.Mail));

            msg.To.Add(
                MailboxAddress.Parse(sendTo));

            var builder = new BodyBuilder
            {
                HtmlBody = message
            };

            msg.Body = builder.ToMessageBody();

            using var client = new SmtpClient();

            await client.ConnectAsync(
                _settings.Host,
                _settings.Port,
                SecureSocketOptions.StartTls);

            await client.AuthenticateAsync(
                _settings.Mail,
                _settings.Password);

            var result = await client.SendAsync(msg);

            await client.DisconnectAsync(true);

            return !result.RejectedRecipients.Any();
        }
    }
}
