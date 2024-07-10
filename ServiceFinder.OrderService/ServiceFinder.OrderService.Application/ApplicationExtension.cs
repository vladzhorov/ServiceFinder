using Microsoft.Extensions.DependencyInjection;
using ServiceFinder.OrderService.Application.EventHandlers;
using ServiceFinder.OrderService.Application.Messaging;

namespace ServiceFinder.OrderService.Application
{
    public static class ApplicationExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<OrderRequestStatusChangedEventHandler>();

            services.AddSingleton<MessagePublisher>(sp =>
                new MessagePublisher("rabbitmq", "order_queue"));

            services.AddScoped<OrderAppService>();
        }
    }
}
