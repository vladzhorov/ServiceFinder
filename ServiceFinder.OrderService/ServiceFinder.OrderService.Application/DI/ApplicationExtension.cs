using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ServiceFinder.OrderService.Application.EventHandlers;
using ServiceFinder.OrderService.Application.Interfaces;
using ServiceFinder.Shared.Events;

namespace ServiceFinder.OrderService.Application.DI
{
    public static class ApplicationExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<OrderStatusChangedEventHandler>());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<OrderRequestStatusChangedEventHandler>());
            services.AddScoped<INotificationHandler<OrderRequestStatusChangedEvent>, OrderRequestStatusChangedEventHandler>();
            services.AddScoped<INotificationHandler<OrderStatusChangedEvent>, OrderStatusChangedEventHandler>();
            services.AddScoped<IOrderAppService, OrderAppService>();
            services.AddScoped<IOrderRequestAppService, OrderRequestAppService>();
        }
    }
}
