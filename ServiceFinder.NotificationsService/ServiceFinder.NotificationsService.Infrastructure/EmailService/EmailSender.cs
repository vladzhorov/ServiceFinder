using Microsoft.Extensions.Options;
using ServiceFinder.NotificationService.Domain.Interfaces;
using ServiceFinder.NotificationsService.Domain.Settings;
using System.Net;
using System.Net.Mail;

namespace ServiceFinder.NotificationService.Infrastructure.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly SmtpClient _smtpClient;
        private readonly EmailSettings _emailSettings;

        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
            _smtpClient = new SmtpClient(_emailSettings.Host)
            {
                Port = _emailSettings.Port,
                Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password),
                EnableSsl = true
            };
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            var mailMessage = new MailMessage(_emailSettings.SenderEmail!, email, subject, message)
            {
                IsBodyHtml = true
            };
            return _smtpClient.SendMailAsync(mailMessage);
        }
    }
}
