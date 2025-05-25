using Microsoft.Extensions.DependencyInjection;
using MyFirstApi.Business.Services;
using MyFirstApi.Repositories.Interfaces;
using MyFirstApi.Services;
using MyFirstApi.Services.Interfaces;

namespace MyFirstApi.Business
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserLoginLogoutService, UserLoginLogoutService>();
            services.AddScoped<IProductService,ProductService>();
            services.AddScoped<IOrderService,OrderService>();
            services.AddScoped<IDeleteOrderService, DeleteOrderService>();
            // Add other services here
            return services;
        }
    }
}
