using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceFinder.OrderService.Application.EventHandlers;
using ServiceFinder.OrderService.Application.Interfaces;
using ServiceFinder.OrderService.Application.Messaging;

namespace ServiceFinder.OrderService.Application
{
    public static class ApplicationExtension
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMQConfiguration>(options => configuration.GetSection("RabbitMQ").Bind(options));
            services.AddSingleton<MessagePublisher>();
            services.AddScoped<IOrderAppService, OrderAppService>();
            services.AddScoped<IOrderRequestAppService, OrderRequestAppService>();
            services.AddSingleton<OrderRequestStatusChangedEventHandler>();
            services.AddSingleton<OrderStatusChangedEventHandler>();
        }
    }
}
