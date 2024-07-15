using Microsoft.Extensions.DependencyInjection;
using ServiceFinder.OrderService.Application.EventHandlers;
using ServiceFinder.OrderService.Application.Interfaces;
using ServiceFinder.OrderService.Domain.Services;

namespace ServiceFinder.OrderService.Application
{
    public static class ApplicationExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IOrderRequestStatusChangedEventHandler, OrderRequestStatusChangedEventHandler>();
            services.AddScoped<IOrderStatusChangedEventHandler, OrderStatusChangedEventHandler>();
            services.AddScoped<IOrderAppService, OrderAppService>();
            services.AddScoped<IOrderRequestAppService, OrderRequestAppService>();

            services.AddScoped<Domain.Services.OrderService>();
            services.AddScoped<OrderRequestService>();
        }
    }
}
