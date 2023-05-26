using Microsoft.Extensions.DependencyInjection;
using Simpress.Product.Domain.Adapters;
using Simpress.Product.Infra.Database.Repositories;
using Simpress.Product.Infra.Database.UoW;

namespace Simpress.Product.Infra.Database
{
    public static class DatabaseModuleDependecy
    {
        public static void AddDatabaseModule(this IServiceCollection services)
        {
            /* Repositories Dependency Injection */

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
        }
    }
}
