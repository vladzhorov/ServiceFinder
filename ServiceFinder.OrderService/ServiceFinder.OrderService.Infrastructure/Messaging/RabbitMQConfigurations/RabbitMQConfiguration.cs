namespace ServiceFinder.OrderService.Domain.Messaging.RabbitMQConfigurations
{
    public class RabbitMQConfiguration
    {
        public RabbitMQConnectionConfiguration? Connection { get; set; }
        public RabbitMQExchangeConfiguration? Exchange { get; set; }
        public RabbitMQQueueConfiguration? MessageQueue { get; set; }
    }
}
