using AutoMapper;
using Simpress.Product.Domain.Adapters;
using Simpress.Product.Domain.Entities.DTO;
using Simpress.Product.Domain.Entities.Models.Request;
using Simpress.Product.Domain.Entities.Models.Validations;
using Simpress.Product.Domain.Services;

namespace Simpress.Product.Application.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly INotificator _notificator;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IProductRepository _repository;
        private readonly ICategoryRepository _categoryRepository;

        #region Constructor
        public ProductService(INotificator notificator,
            IUnitOfWork uow,
            IMapper mapper,
            IProductRepository repository,
            ICategoryRepository categoryRepository) : base(notificator)
        {
            _uow = uow;
            _mapper = mapper;
            _repository = _uow.ProductRepository;
            _notificator = notificator;
            _categoryRepository = _uow.CategoryRepository;
        }
        #endregion
        
        private async Task<bool> ExistsCategoryAsync(int id)
        {
            var category = await _categoryRepository.GetById(id);

            return category != null;
        }

        public async Task<RequestResult> Add(ProductDto entity)
        {
            var product = _mapper.Map<Domain.Entities.Models.Product>(entity);

            if(!ExecuteValidation(new ProductValidation(), product))
            {
                return new RequestResult
                {
                    Success = false,
                    Data = _notificator.GetAllNotifications()
                };
                
            }

            var categoryExists = await ExistsCategoryAsync(product.CategoryId);

            if(!categoryExists) return new RequestResult { Success = false, Data = "Category not Found" };

            await _repository.Add(product);

            _uow.Save();

            return new RequestResult { Success = true, Data = product };
        }

        public async Task<RequestResult> GetAll()
        {
            return new RequestResult { Data = await _repository.GetAll(), Success = true };
        }

        public async Task<RequestResult> GetById(int id)
        {
            var product = await _repository.GetById(id);

            if(product is null)
            {
                return new RequestResult() { Success = false, Data = "Product Not Found" };
            }

            return new RequestResult() { Success = true, Data = product };
        }

        public async Task<RequestResult> Remove(int id)
        {
            var product = await _repository.GetById(id);

            if (product is null)
            {
                return new RequestResult() { Success = false, Data = "Product Not Found" };
            }

            await _repository.Remove(product);

            return new RequestResult() { Success = true, Data = "Product removed successfully" };
        }

        public async Task<RequestResult> Update(ProductDto entity, int id)
        {
            var product = await _repository.GetById(id);

            if (product is null)
            {
                return new RequestResult() { Success = false, Data = "Product Not Found" };
            }

            product.UpdateProduct(entity.Description, entity.Price, entity.CategoryId);

            if (!ExecuteValidation(new ProductValidation(), product))
            {
                return new RequestResult
                {
                    Success = false,
                    Data = _notificator.GetAllNotifications()
                };

            }

            var categoryExists = await ExistsCategoryAsync(product.CategoryId);

            if (!categoryExists) return new RequestResult { Success = false, Data = "Category not Found" };

            await _repository.Update(product);

            _uow.Save();

            return new RequestResult() { Success = true, Data = product };
        }
        


    }
}
