using Microsoft.Extensions.DependencyInjection;
using ServiceFinder.OrderService.Domain.Interfaces;
using ServiceFinder.OrderService.Domain.Services;

namespace ServiceFinder.OrderService.Domain
{
    public static class DomainExtension
    {
        public static void AddDomain(this IServiceCollection services)
        {
            services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
            services.AddScoped<IOrderService, Services.OrderService>();
            services.AddScoped<IOrderRequestService, OrderRequestService>();
        }
    }
}
