using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ServiceFinder.NotificationService.Application.Consumers;
using ServiceFinder.NotificationService.Domain.Interfaces;
using ServiceFinder.NotificationService.Infrastructure.Services;
using ServiceFinder.NotificationsService.Domain.Settings;
using ServiceFinder.NotificationsService.Infrastructure;
using ServiceFinder.NotificationsService.Infrastructure.Constants;

namespace ServiceFinder.NotificationService.Infrastructure.DI
{
    public static class RabbitMQConfiguration
    {
        private static void AddRabbitMQ(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMqSettings>(options =>
                configuration.GetSection(RabbitMqConstants.RabbitMQ).Bind(options));

            services.AddSingleton(serviceProvider =>
                serviceProvider.GetRequiredService<IOptions<RabbitMqSettings>>().Value);

            services.AddMassTransit(busConfiguration =>
            {
                busConfiguration.SetKebabCaseEndpointNameFormatter();

                busConfiguration.AddConsumer<OrderStatusChangedEventConsumer>();
                busConfiguration.AddConsumer<OrderRequestStatusChangedEventConsumer>();

                busConfiguration.UsingRabbitMq((context, cfg) =>
                {
                    var settings = context.GetRequiredService<RabbitMqSettings>();

                    cfg.Host(new Uri(settings.Host), hostConfigure =>
                    {
                        hostConfigure.Username(settings.Username);
                        hostConfigure.Password(settings.Password);
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });
        }

        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(options =>
               configuration.GetSection(RabbitMqConstants.EmailSettings).Bind(options));

            services.AddTransient<IEmailSender, EmailSender>();

            services.AddRabbitMQ(configuration);
        }
    }
}
