using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceFinder.NotificationService.Application.Consumers;
using ServiceFinder.NotificationService.Application.Interfaces;
using ServiceFinder.NotificationService.Application.Services;
using ServiceFinder.NotificationsService.Domain.Interfaces;

namespace ServiceFinder.NotificationService.Application.DI
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<INotificationService, Services.NotificationService>();
            services.AddTransient<IEventBus, EventBus>();
            services.AddScoped<NotificationConsumer>();

            return services;
        }
    }
}
