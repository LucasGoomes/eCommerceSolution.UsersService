using eCommerce.Core.ServiceContracts;
using eCommerce.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Core
{
    public static class DependencyInjection
    {
        /// <summary>
        ///  Extension method to add Core services to the dependency injection container.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            // TO DO: Add services to the IoC container
            services.AddTransient<IUsersService, UsersService>();

            return services;
        }
    }
}
