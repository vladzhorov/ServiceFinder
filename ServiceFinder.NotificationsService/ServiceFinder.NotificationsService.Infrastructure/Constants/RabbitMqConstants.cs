namespace ServiceFinder.NotificationsService.Infrastructure.Constants
{
    public static class RabbitMqConstants
    {
        public const string RabbitMQ = "RabbitMQ:";
        public const string Host = RabbitMQ + "Host";
        public const string Username = RabbitMQ + "Username";
        public const string Password = RabbitMQ + "Password";
        public const string NotificationQueue = "notification-queue";
    }
}
