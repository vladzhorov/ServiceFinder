using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ServiceFinder.OrderService.Domain.Interfaces;
using ServiceFinder.OrderService.Infrastructure.Repositories;

namespace ServiceFinder.OrderService.Infrastructure
{
    public static class InfrastructureExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseOptions>(options => configuration.GetSection(nameof(DatabaseOptions)).Bind(options));

            services.AddDbContext<OrderDbContext>((serviceProvider, options) =>
            {
                var dbOptions = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>().Value;

                options.UseNpgsql(dbOptions.ConnectionString);
            });

            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderRequestRepository, OrderRequestRepository>();
        }
    }
}
