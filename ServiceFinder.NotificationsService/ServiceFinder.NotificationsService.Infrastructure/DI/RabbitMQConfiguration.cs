using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceFinder.NotificationService.Domain.Interfaces;
using ServiceFinder.NotificationService.Infrastructure.RabbitMQ;
using ServiceFinder.NotificationService.Infrastructure.Services;
using ServiceFinder.NotificationsService.Domain.Settings;
using ServiceFinder.NotificationsService.Infrastructure.Constants;

namespace ServiceFinder.NotificationService.Infrastructure.DI
{
    public static class RabbitMQConfiguration
    {
        public static IServiceCollection AddRabbitMQ(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddMassTransit(x =>
            {
                x.AddConsumer<NotificationConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    var host = configuration[RabbitMqConstants.Host];
                    var username = configuration[RabbitMqConstants.Username];
                    var password = configuration[RabbitMqConstants.Password];

                    if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                    {
                        throw new ArgumentNullException("RabbitMQ configuration values cannot be null or empty.");
                    }

                    cfg.Host(host, h =>
                    {
                        h.Username(username);
                        h.Password(password);
                    });

                    cfg.ReceiveEndpoint(RabbitMqConstants.NotificationQueue, e =>
                    {
                        e.ConfigureConsumer<NotificationConsumer>(context);
                    });
                });
            });
            return services;
        }
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(options =>
               configuration.GetSection("Email").Bind(options));
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddRabbitMQ(configuration);

            return services;
        }
    }
}
