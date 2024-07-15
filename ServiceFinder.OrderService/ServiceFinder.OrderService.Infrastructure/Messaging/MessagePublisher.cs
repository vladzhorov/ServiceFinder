using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using ServiceFinder.OrderService.Domain.Messaging.RabbitMQConfigurations;
using System.Text;

namespace ServiceFinder.OrderService.Domain.Messaging
{
    public class MessagePublisher : IMessagePublisher
    {
        private readonly IModel _channel;
        private readonly string _exchangeName;
        private readonly string _routingKey;

        public MessagePublisher(IModel channel, IOptions<RabbitMQConfiguration> config)
        {
            _channel = channel;
            var configuration = config.Value;
            _exchangeName = configuration.Exchange!.Name;
            _routingKey = configuration.MessageQueue!.RoutingKey;
        }

        public void Publish(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: _exchangeName,
                                 routingKey: _routingKey,
                                 basicProperties: null,
                                 body: body);
            Console.WriteLine($" [x] Sent {message}");
        }

        public void Dispose()
        {
            _channel.Close();
        }
    }
}
