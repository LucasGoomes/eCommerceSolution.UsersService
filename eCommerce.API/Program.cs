using eCommerce.Infrastructure;
using eCommerce.Core;
using eCommerce.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add Infrastructure services to the container
builder.Services.AddInfrastructure();
builder.Services.AddCore();

// Add controllers
builder.Services.AddControllers();

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
