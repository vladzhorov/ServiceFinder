namespace ServiceFinder.OrderService.Application.Messaging.RabbitMQConfigurations
{
    public class RabbitMQConnectionConfiguration
    {
        public string HostName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
