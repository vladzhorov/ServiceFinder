using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ServiceFinder.DAL.Interfaces;
using ServiceFinder.DAL.Repositories;

namespace ServiceFinder.DAL
{
    public static class DalServiceExtension
    {
        public static void AddDALDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseOptions>(options => configuration.GetSection(nameof(DatabaseOptions)).Bind(options));

            services.AddDbContext<AppDbContext>((serviceProvider, options) =>
            {
                var dbOptions = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>().Value;

                options.UseNpgsql(dbOptions.ConnectionString);

            });
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IAssistanceRepository, AssistanceRepository>();
            services.AddScoped<IAssistanceCategoryRepository, AssistanceCategoryRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
        }
    }
}