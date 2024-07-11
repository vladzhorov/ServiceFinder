namespace ServiceFinder.OrderService.Application.Messaging.RabbitMQConfigurations
{
    public class RabbitMQQueueConfiguration
    {
        public string Name { get; set; } = string.Empty;
        public string RoutingKey { get; set; } = string.Empty;
    }
}