using AutoMapper;
using Simpress.Product.Domain.Adapters;
using Simpress.Product.Domain.Entities.DTO;
using Simpress.Product.Domain.Entities.Models;
using Simpress.Product.Domain.Entities.Models.Request;
using Simpress.Product.Domain.Entities.Models.Validations;
using Simpress.Product.Domain.Services;

namespace Simpress.Product.Application.Services
{
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly INotificator _notificator;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _repository;
        public CategoryService(INotificator notificator, IUnitOfWork uow, IMapper mapper, ICategoryRepository repository) : base(notificator)
        {
            _uow = uow;
            _mapper = mapper;
            _repository = _uow.CategoryRepository;
            _notificator = notificator;
        }

        public async Task<RequestResult> Add(CategoryDto entity)
        {
            var category = _mapper.Map<Category>(entity);

            if (!ExecuteValidation(new CategoryValidation(), category))
            {
                return new RequestResult
                {
                    Success = false,
                    Data = _notificator.GetAllNotifications()
                };

            }

            await _repository.Add(category);

            _uow.Save();

            return new RequestResult { Success = true, Data = category };
        }

        public async Task<RequestResult> GetAll()
        {
            return new RequestResult { Data = await _repository.GetAll(), Success = true };
        }

        public async Task<RequestResult> GetById(int id)
        {
            var category = await _repository.GetById(id);

            if (category is null)
            {
                return new RequestResult() { Success = false, Data = "Category Not Found" };
            }

            return new RequestResult() { Success = true, Data = category };
        }

        public async Task<RequestResult> Remove(int id)
        {
            var category = await _repository.GetById(id);

            if (category is null)
            {
                return new RequestResult() { Success = false, Data = "Category Not Found" };
            }

            await _repository.Remove(category);

            return new RequestResult() { Success = true, Data = category };
        }

        public async Task<RequestResult> Update(CategoryDto entity, int id)
        {
            var category = await _repository.GetById(id);

            if (category is null)
            {
                return new RequestResult() { Success = false, Data = "Category Not Found" };
            }

            category.UpdateCategory(entity.Description);

            if (!ExecuteValidation(new CategoryValidation(), category))
            {
                return new RequestResult
                {
                    Success = false,
                    Data = _notificator.GetAllNotifications()
                };

            }

            await _repository.Update(category);

            _uow.Save();

            return new RequestResult() { Success = true, Data = category };
        }
    }
}
