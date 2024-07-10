using RabbitMQ.Client;
using System.Text;

namespace ServiceFinder.OrderService.Application.Messaging
{
    public class MessagePublisher : IMessagePublisher
    {
        private readonly IModel _channel;
        private readonly string _exchangeName;
        private readonly string _routingKey;

        public MessagePublisher(IModel channel, string exchangeName, string routingKey)
        {
            _channel = channel;
            _exchangeName = exchangeName;
            _routingKey = routingKey;
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
