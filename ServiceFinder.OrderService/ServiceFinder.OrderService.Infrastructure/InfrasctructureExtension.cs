using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using ServiceFinder.OrderService.Domain.Constants;
using ServiceFinder.OrderService.Domain.Interfaces;
using ServiceFinder.OrderService.Domain.Messaging;
using ServiceFinder.OrderService.Domain.Messaging.RabbitMQConfigurations;
using ServiceFinder.OrderService.Domain.Providers;
using ServiceFinder.OrderService.Domain.Services;
using ServiceFinder.OrderService.Infrastructure.Repositories;

namespace ServiceFinder.OrderService.Infrastructure
{
    public static class InfrastructureExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseOptions>(options =>
                configuration.GetSection(nameof(DatabaseOptions)).Bind(options));

            services.AddDbContext<OrderDbContext>((serviceProvider, options) =>
            {
                var dbOptions = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>().Value;
                options.UseNpgsql(dbOptions.ConnectionString);
            });

            services.Configure<RabbitMQConfiguration>(options =>
                configuration.GetSection(RabbitMQConfigurationConstants.RabbitMQSection).Bind(options));

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
                channel.QueueDeclare(queue: options.MessageQueue!.Name,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                channel.QueueBind(queue: options.MessageQueue.Name,
                                  exchange: options.Exchange.Name,
                                  routingKey: options.MessageQueue.RoutingKey);

                return channel;
            });

            services.AddScoped<IMessagePublisher>(provider =>
            {
                var options = provider.GetRequiredService<IOptions<RabbitMQConfiguration>>().Value;
                var channel = provider.GetRequiredService<IModel>();
                return new MessagePublisher(channel, options.Exchange!.Name, options.MessageQueue!.RoutingKey);
            });

            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderRequestRepository, OrderRequestRepository>();
            services.AddScoped<IDateTimeProvider, DateTimeProvider>();
            services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

        }
    }
}
