using Simpress.Product.Domain.Adapters;
using Simpress.Product.Infra.Database.Context;

namespace Simpress.Product.Infra.Database.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SimpressContext _context;

        public UnitOfWork(SimpressContext context, ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            _context = context;
            CategoryRepository = categoryRepository;
            ProductRepository = productRepository;
        }

        public ICategoryRepository CategoryRepository { get; private set; }

        public IProductRepository ProductRepository {get; private set; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
