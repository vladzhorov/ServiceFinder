using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceFinder.NotificationsService.Application.EventHandlers;
using ServiceFinder.NotificationsService.Domain.Interfaces;

namespace ServiceFinder.NotificationService.Application.DI
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<OrderRequestStatusChangedEventHandler>());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<OrderStatusChangedEventHandler>());
            services.AddTransient<INotificationService, Services.NotificationService>();

            return services;
        }
    }
}
