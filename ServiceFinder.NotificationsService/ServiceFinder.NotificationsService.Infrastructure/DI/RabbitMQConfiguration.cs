using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceFinder.NotificationsService.Infrastructure.Constants;

namespace ServiceFinder.NotificationService.Infrastructure.RabbitMQ
{
    public static class RabbitMQConfiguration
    {
        public static IServiceCollection AddRabbitMQ(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<NotificationConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(configuration[RabbitMqConstants.Host], h =>
                    {
                        h.Username(configuration[RabbitMqConstants.Username]);
                        h.Password(configuration[RabbitMqConstants.Password]);
                    });

                    cfg.ReceiveEndpoint(RabbitMqConstants.NotificationQueue, e =>
                    {
                        e.ConfigureConsumer<NotificationConsumer>(context);
                    });
                });
            });

            services.AddMassTransitHostedService();
            return services;
        }
    }
}
