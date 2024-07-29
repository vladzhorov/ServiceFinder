using ServiceFinder.NotificationsService.Domain.Exceptions;
using ServiceFinder.NotificationsService.Domain.Models;
using System.Net.Mail;

namespace ServiceFinder.NotificationsService.Domain.Validation
{
    public static class NotificationValidator
    {
        public static void Validate(SendEmailCommand notification)
        {
            if (notification == null)
                throw new NotificationException("Notification cannot be null.");

            if (string.IsNullOrWhiteSpace(notification.Email) || !IsValidEmail(notification.Email))
                throw new NotificationException("Valid recipient email is required.");

            if (string.IsNullOrWhiteSpace(notification.Subject))
                throw new NotificationException("Subject is required.");

            if (string.IsNullOrWhiteSpace(notification.Body))
                throw new NotificationException("Body is required.");
        }

        private static bool IsValidEmail(string email)
        {
            try
            {
                var mail = new MailAddress(email);
                return mail.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}