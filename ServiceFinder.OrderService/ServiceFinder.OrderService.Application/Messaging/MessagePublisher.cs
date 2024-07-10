using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;

namespace ServiceFinder.OrderService.Application.Messaging
{
    public class MessagePublisher : IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _queueName;

        public MessagePublisher(IConfiguration configuration)
        {
            var hostname = configuration["RabbitMQ:HostName"];
            var username = configuration["RabbitMQ:UserName"];
            var password = configuration["RabbitMQ:Password"];
            _queueName = configuration["RabbitMQ:QueueName"];

            var factory = new ConnectionFactory()
            {
                HostName = hostname,
                UserName = username,
                Password = password
            };

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
