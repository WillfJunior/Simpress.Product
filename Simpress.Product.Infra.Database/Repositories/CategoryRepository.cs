using Simpress.Product.Domain.Adapters;
using Simpress.Product.Domain.Entities.Models;
using Simpress.Product.Infra.Database.Context;

namespace Simpress.Product.Infra.Database.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(SimpressContext? context) : base(context)
        {
        }
    }
}
