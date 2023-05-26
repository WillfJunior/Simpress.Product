using Simpress.Product.Domain.Entities.Models.Request;

namespace Simpress.Product.Domain.Services
{
    public interface IServiceBase<T>
    {
        Task<RequestResult> Add(T entity);
        Task<RequestResult> GetAll();
        Task<RequestResult> Update(T entity, int id);
        Task<RequestResult> GetById(int id);
        Task<RequestResult> Remove(int id);
    }
}
