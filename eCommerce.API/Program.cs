using eCommerce.Infrastructure;
using eCommerce.Core;
using eCommerce.API.Middlewares;
using System.Text.Json.Serialization;
using eCommerce.Core.Mappers;

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

var app = builder.Build();

// Middleware for exception handling
app.UseExceptionHandlingMiddleware();

// Routing
app.UseRouting();

// Auth
app.UseAuthentication();
app.UseAuthorization();

// Controller routes
app.MapControllers();

app.Run();
