using ServiceFinder.NotificationsService.Domain.Enums;

namespace ServiceFinder.NotificationsService.Domain.Models
{
    public class Notification
    {
        public Guid Id { get; set; }
        public string? Email { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public NotificationType Type { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
