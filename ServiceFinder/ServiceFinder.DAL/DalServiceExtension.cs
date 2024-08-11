using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ServiceFinder.DAL.Interfaces;
using ServiceFinder.DAL.RabbitMQConfigurations;
using ServiceFinder.DAL.Repositories;

namespace ServiceFinder.DAL
{
    public static class DalServiceExtension
    {
        public static void AddDALDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseOptions>(options => configuration.GetSection(nameof(DatabaseOptions)).Bind(options));
            services.Configure<RabbitMQConfiguration>(options =>
               configuration.GetSection("RabbitMQ").Bind(options));
            services.AddSingleton(serviceProvider =>
            serviceProvider.GetRequiredService<IOptions<RabbitMQConfiguration>>().Value);

            services.AddMassTransit(busConfiguration =>
            {
                busConfiguration.SetKebabCaseEndpointNameFormatter();

                busConfiguration.UsingRabbitMq((context, cfg) =>
                {
                    var settings = context.GetRequiredService<RabbitMQConfiguration>();

                    cfg.Host(new Uri(settings.Connection!.Host), hostConfigure =>
                    {
                        hostConfigure.Username(settings.Connection.UserName);
                        hostConfigure.Password(settings.Connection.Password);
                    });
                    cfg.ConfigureEndpoints(context);
                });
            });
            services.AddDbContext<AppDbContext>((serviceProvider, options) =>
            {
                var dbOptions = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>().Value;

                options.UseNpgsql(dbOptions.ConnectionString).EnableSensitiveDataLogging()
            .LogTo(Console.WriteLine, LogLevel.Information);

            });
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IAssistanceRepository, AssistanceRepository>();
            services.AddScoped<IAssistanceCategoryRepository, AssistanceCategoryRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
        }
    }
}