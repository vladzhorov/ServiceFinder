using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ServiceFinder.DAL.Interceptors;
using ServiceFinder.DAL.Interfaces;
using ServiceFinder.DAL.Repositories;

namespace ServiceFinder.DAL
{
    public static class DalServiceExtension
    {
        public static void AddDALDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseOptions>(options => configuration.GetSection(nameof(DatabaseOptions)).Bind(options));
            services.AddScoped<UpdateAuditableInterceptor>();
            services.AddScoped<SoftDeleteInterceptor>();

            services.AddDbContext<AppDbContext>((serviceProvider, options) =>
            {
                var dbOptions = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>().Value;

                options.UseNpgsql(dbOptions.ConnectionString)
                 .AddInterceptors(
                    serviceProvider.GetRequiredService<UpdateAuditableInterceptor>(),
                    serviceProvider.GetRequiredService<SoftDeleteInterceptor>());

            });
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IServiceCategoryRepository, ServiceCategoryRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
        }
    }
}