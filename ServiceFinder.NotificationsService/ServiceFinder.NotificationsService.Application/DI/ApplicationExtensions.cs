using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceFinder.NotificationService.Application.Services;
using ServiceFinder.NotificationsService.Application.EventHandlers;

namespace ServiceFinder.NotificationService.Application.DI
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<SendEmailCommandHandler>());
            services.AddHostedService<NotificationListener>();
            return services;
        }
    }
}
