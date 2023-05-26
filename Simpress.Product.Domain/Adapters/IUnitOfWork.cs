namespace Simpress.Product.Domain.Adapters
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get;  }
        IProductRepository ProductRepository { get;  }

        bool Save();
        void Dispose();
    }
}
