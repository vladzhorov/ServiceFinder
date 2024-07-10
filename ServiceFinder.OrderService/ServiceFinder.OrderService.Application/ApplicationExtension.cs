using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using ServiceFinder.OrderService.Application.EventHandlers;
using ServiceFinder.OrderService.Application.Interfaces;
using ServiceFinder.OrderService.Application.Messaging;
using ServiceFinder.OrderService.Application.Messaging.RabbitMQConfigurations;

namespace ServiceFinder.OrderService.Application
{
    public static class ApplicationExtension
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMQConfiguration>(options => configuration.GetSection("RabbitMQ").Bind(options));
            services.AddSingleton(provider =>
            {
                var options = provider.GetRequiredService<IOptions<RabbitMQConfiguration>>().Value;
                var factory = new ConnectionFactory()
                {
                    HostName = options.Connection!.HostName,
                    UserName = options.Connection.UserName,
                    Password = options.Connection.Password
                };
                var connection = factory.CreateConnection();
                var channel = connection.CreateModel();

                channel.ExchangeDeclare(exchange: options.Exchange!.Name, type: options.Exchange.Type);
                channel.QueueDeclare(queue: options.Queue!.Name,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                channel.QueueBind(queue: options.Queue.Name,
                                  exchange: options.Exchange.Name,
                                  routingKey: options.Queue.RoutingKey);

                return channel;
            });

            services.AddScoped<IMessagePublisher, MessagePublisher>();
        }

        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IOrderAppService, OrderAppService>();
            services.AddScoped<IOrderRequestAppService, OrderRequestAppService>();
            services.AddScoped<OrderRequestStatusChangedEventHandler>();
            services.AddScoped<OrderStatusChangedEventHandler>();
        }
    }
}
