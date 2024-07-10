using RabbitMQ.Client;
using System.Text;

namespace ServiceFinder.OrderService.Application.Messaging
{
    public class MessagePublisher : IMessagePublisher
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _exchangeName;
        private readonly string _queueName;
        private readonly string _routingKey;

        public MessagePublisher(IConnection connection, IModel channel, string exchangeName, string queueName, string routingKey)
        {
            _connection = connection;
            _channel = channel;
            _exchangeName = exchangeName;
            _queueName = queueName;
            _routingKey = routingKey;

            _channel.ExchangeDeclare(exchange: _exchangeName, type: "direct");

            _channel.QueueDeclare(queue: _queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            _channel.QueueBind(queue: _queueName,
                              exchange: _exchangeName,
                              routingKey: _routingKey);
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
            _connection.Close();
        }
    }
}
