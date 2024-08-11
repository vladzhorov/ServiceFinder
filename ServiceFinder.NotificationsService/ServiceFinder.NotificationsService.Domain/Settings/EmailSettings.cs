
namespace ServiceFinder.NotificationsService.Domain.Settings
{
    public class EmailSettings
    {
        public string? SenderEmail { get; set; }
        public string? Host { get; set; }
        public int Port { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
