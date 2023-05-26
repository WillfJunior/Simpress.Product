using Microsoft.Extensions.DependencyInjection;
using Simpress.Product.Application.AutoMapperConfig;
using Simpress.Product.Application.Services;
using Simpress.Product.Domain.Services;

namespace Simpress.Product.Application
{
    public static class ApplicationModuleDependency
    {
        public static void AddApplicationModule(this IServiceCollection services)
        {
            services.AddScoped<INotificator, Notificator>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService,CategoryService>();
            services.AddAutoMapper(typeof(AutoMapperConfiguration));
        }
    }
}
