using FluentValidation;
using Microsoft.OpenApi.Models;
using ServiceFinder.API.Mapper;
using ServiceFinder.API.Middleware;
using ServiceFinder.API.Validators.Assistance;
using ServiceFinder.BLL;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = " API ", Version = "v1" });
});

builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssemblyContaining<CreateAssistanceViewModelValidator>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(Mapping));
builder.Services.AddSwaggerGen();

builder.Services.AddBLLDependencies(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
