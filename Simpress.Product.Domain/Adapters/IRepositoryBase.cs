using Simpress.Product.Domain.Entities;

namespace Simpress.Product.Domain.Adapters
{
    public interface IRepositoryBase<T> where T : BaseEntity
    {
        Task Add(T entity);
        Task Update(T entity);
        Task<T> GetById(int id);
        Task Remove(T entity);
        Task<List<T>> GetAll();
       
    }
}
