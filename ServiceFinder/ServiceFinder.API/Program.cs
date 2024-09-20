using FluentValidation;
using FluentValidation.Resources;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using ServiceFinder.API.Mapper;
using ServiceFinder.API.Middleware;
using ServiceFinder.API.Validators.Assistance;
using ServiceFinder.BLL;
using ServiceFinder.BLL.Abstarctions.Services;
using ServiceFinder.BLL.Services;
using System.Globalization;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin",
                policy =>
                {
                    policy.WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
        });

        builder.Services.AddControllers();

        builder.Services.AddValidatorsFromAssemblyContaining<CreateAssistanceViewModelValidator>();

        builder.Services.AddAutoMapper(typeof(Mapping));

        ValidatorOptions.Global.LanguageManager = new LanguageManager
        {
            Culture = new CultureInfo("en")
        };

        builder.Services.AddBLLDependencies(builder.Configuration);

        builder.Services.AddScoped<IAuthServiceClient, AuthServiceClient>();
        builder.Services.AddHttpClient<AuthServiceClient>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:7292");
        });

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.Authority = $"https://{builder.Configuration["Auth0:Domain"]}/";
            options.Audience = builder.Configuration["Auth0:Audience"];
            options.RequireHttpsMetadata = false;

            // Добавьте эту настройку для проверки разрешений (scopes)
            options.Events = new JwtBearerEvents
            {
                OnTokenValidated = context =>
                {
                    var user = context.Principal;
                    return Task.CompletedTask;
                }
            };
        });


        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminPolicy", policy =>
                policy.RequireClaim("permissions", "read:admin"));

            options.AddPolicy("UserPolicy", policy =>
                policy.RequireClaim("permissions", "read:user"));
        });


        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCors("AllowSpecificOrigin");

        app.UseMiddleware<ExceptionMiddleware>();

        app.MapControllers();

        app.Run();
    }
}
