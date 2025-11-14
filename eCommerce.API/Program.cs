using eCommerce.Infrastructure;
using eCommerce.Core;
using eCommerce.API.Middlewares;
using System.Text.Json.Serialization;
using eCommerce.Core.Mappers;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add Infrastructure services to the container
builder.Services.AddInfrastructure();
builder.Services.AddCore();

// Add controllers
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
                                // pass the assemble reference where mapping profiles are defined/exist
builder.Services.AddAutoMapper(typeof(ApplicationUserMappingProfile).Assembly);

// Fluent validations
builder.Services.AddFluentValidationAutoValidation();

//add api explorer services - used by swagger
builder.Services.AddEndpointsApiExplorer();

//add swagger generation services
builder.Services.AddSwaggerGen();

// add cors services
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

// Middleware for exception handling
app.UseExceptionHandlingMiddleware();

// Routing
app.UseRouting();
app.UseSwagger(); // add endpoint to serve swagger.json
app.UseSwaggerUI();

// add cors
app.UseCors();

// Auth
app.UseAuthentication();
app.UseAuthorization();

// Controller routes
app.MapControllers();

app.Run();
