using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceFinder.OrderService.Application.EventHandlers;
using ServiceFinder.OrderService.Application.Messaging;

namespace ServiceFinder.OrderService.Application
{
    public static class ApplicationExtension
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<OrderRequestStatusChangedEventHandler>();

            services.AddSingleton<MessagePublisher>(sp => new MessagePublisher(configuration));

            services.AddScoped<OrderAppService>();
        }
    }
}
