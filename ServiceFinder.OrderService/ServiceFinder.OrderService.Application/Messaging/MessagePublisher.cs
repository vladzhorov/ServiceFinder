using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Text;

namespace ServiceFinder.OrderService.Application.Messaging
{
    public class MessagePublisher : IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _exchangeName;
        private readonly string _queueName;
        private readonly string _routingKey;

        public MessagePublisher(IOptions<RabbitMQConfiguration> options)
        {
            var configuration = options.Value;

            var factory = new ConnectionFactory()
            {
                HostName = configuration.HostName,
                UserName = configuration.UserName,
                Password = configuration.Password
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _exchangeName = configuration.ExchangeName;
            _queueName = configuration.QueueName;
            _routingKey = configuration.RoutingKey;

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
