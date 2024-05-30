using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using ServiceFinder.API.Mapper;
using ServiceFinder.API.Validators.Assistance;
using ServiceFinder.API.Validators.AssistanceCategory;
using ServiceFinder.API.Validators.Review;
using ServiceFinder.API.Validators.UserProfile;
using ServiceFinder.BLL;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = " API ", Version = "v1" });
});

builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserProfileViewModelValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateReviewViewModelValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateAssistanceViewModelValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateAssistanceCategoryViewModelValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateUserProfileViewModelValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateAssistanceViewModelValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateAssistanceCategoryViewModelValidator>();
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

app.MapControllers();

app.Run();
