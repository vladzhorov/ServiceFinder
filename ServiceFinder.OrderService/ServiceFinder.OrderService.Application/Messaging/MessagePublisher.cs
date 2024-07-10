using RabbitMQ.Client;
using System.Text;

namespace ServiceFinder.OrderService.Application.Messaging
{
    public class MessagePublisher : IDisposable
    {
        private readonly string _hostname;
        private readonly string _queueName;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessagePublisher(string hostname, string queueName)
        {
            _hostname = hostname;
            _queueName = queueName;

            var factory = new ConnectionFactory() { HostName = _hostname };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
        }

        public void Publish(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "",
                                 routingKey: _queueName,
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
