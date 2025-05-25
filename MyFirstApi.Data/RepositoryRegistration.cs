using Microsoft.Extensions.DependencyInjection;
using MyFirstApi.Data.Repositories;
using MyFirstApi.Repositories;
using MyFirstApi.Repositories.Interfaces;

namespace MyFirstApi.Data
{
    public static class RepositoryRegistration
    {
        public static IServiceCollection AddDataRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            // Add other repositories here
            return services;
        }
    }
}
