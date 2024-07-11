namespace ServiceFinder.OrderService.Application.Messaging.RabbitMQConfigurations
{
    public class RabbitMQConfiguration
    {
        public RabbitMQConnectionConfiguration? Connection { get; set; }
        public RabbitMQExchangeConfiguration? Exchange { get; set; }
        public RabbitMQQueueConfiguration? MessageQueue { get; set; }
    }
}
