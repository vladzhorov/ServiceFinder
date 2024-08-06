using FluentValidation;
using FluentValidation.Resources;
using ServiceFinder.API.Validators;
using ServiceFinder.OrderService.API.Mapper;
using ServiceFinder.OrderService.API.Middleware;
using ServiceFinder.OrderService.Application.DI;
using ServiceFinder.OrderService.Application.Mapper;
using ServiceFinder.OrderService.Domain.DI;
using ServiceFinder.OrderService.Infrastructure.DI;
using System.Globalization;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssemblyContaining<CreateOrderViewModelValidator>();
builder.Services.AddDomain();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAutoMapper(typeof(OrderMappingProfile), typeof(OrderViewModelMappingProfile));
ValidatorOptions.Global.LanguageManager = new LanguageManager
{
    Culture = new CultureInfo("en")
};

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();

app.Run();
