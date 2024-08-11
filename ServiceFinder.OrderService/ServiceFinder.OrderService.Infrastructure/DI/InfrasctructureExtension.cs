using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ServiceFinder.OrderService.Domain.Interfaces;
using ServiceFinder.OrderService.Domain.Messaging;
using ServiceFinder.OrderService.Domain.Messaging.RabbitMQConfigurations;
using ServiceFinder.OrderService.Domain.Providers;
using ServiceFinder.OrderService.Infrastructure.Messaging;
using ServiceFinder.OrderService.Infrastructure.Repositories;

namespace ServiceFinder.OrderService.Infrastructure.DI
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
                configuration.GetSection("RabbitMQ").Bind(options));

            services.AddSingleton(serviceProvider =>
                serviceProvider.GetRequiredService<IOptions<RabbitMQConfiguration>>().Value);

            services.AddMassTransit(busConfiguration =>
            {
                busConfiguration.SetKebabCaseEndpointNameFormatter();

                busConfiguration.UsingRabbitMq((context, cfg) =>
                {
                    var settings = context.GetRequiredService<RabbitMQConfiguration>();

                    cfg.Host(new Uri(settings.Connection!.Host), hostConfigure =>
                    {
                        hostConfigure.Username(settings.Connection.UserName);
                        hostConfigure.Password(settings.Connection.Password);
                    });
                    cfg.ConfigureEndpoints(context);
                });
            });

            services.AddScoped<IMessagePublisher, MessagePublisher>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderRequestRepository, OrderRequestRepository>();
            services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        }
    }
}
