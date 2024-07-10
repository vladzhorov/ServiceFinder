using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using ServiceFinder.OrderService.Application.EventHandlers;
using ServiceFinder.OrderService.Application.Interfaces;
using ServiceFinder.OrderService.Application.Messaging;

namespace ServiceFinder.OrderService.Application
{
    public static class ApplicationExtension
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMQConfiguration>(options => configuration.GetSection("RabbitMQ").Bind(options));
            services.AddSingleton<IMessagePublisher>(provider =>
            {
                var config = provider.GetRequiredService<IOptions<RabbitMQConfiguration>>().Value;
                var factory = new ConnectionFactory()
                {
                    HostName = config.HostName,
                    UserName = config.UserName,
                    Password = config.Password
                };

                var connection = factory.CreateConnection();
                var channel = connection.CreateModel();

                return new MessagePublisher(connection, channel, config.ExchangeName, config.QueueName, config.RoutingKey);
            });

            services.AddScoped<IOrderAppService, OrderAppService>();
            services.AddScoped<IOrderRequestAppService, OrderRequestAppService>();
            services.AddSingleton<OrderRequestStatusChangedEventHandler>();
            services.AddSingleton<OrderStatusChangedEventHandler>();
        }
    }
}
