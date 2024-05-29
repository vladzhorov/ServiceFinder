
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceFinder.BLL.Abstarctions.Services;
using ServiceFinder.BLL.Mapper;
using ServiceFinder.BLL.Services;
using ServiceFinder.DAL;


namespace ServiceFinder.BLL
{
    public static class BLLServiceExtension
    {
        public static void AddBLLDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDALDependencies(configuration);

            services.AddAutoMapper(typeof(Mapping));
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IAssistanceService, AssistanceService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IAssistanceCategoryService, AssistanceCategoryService>();
        }
    }
}

