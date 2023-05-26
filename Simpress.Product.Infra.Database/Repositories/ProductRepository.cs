using Simpress.Product.Domain.Adapters;
using Simpress.Product.Infra.Database.Context;

namespace Simpress.Product.Infra.Database.Repositories
{
    public class ProductRepository : RepositoryBase<Domain.Entities.Models.Product>, IProductRepository
    {
        public ProductRepository(SimpressContext? context) : base(context)
        {
        }
    }
}
