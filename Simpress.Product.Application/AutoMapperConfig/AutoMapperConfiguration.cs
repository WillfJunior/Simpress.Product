using AutoMapper;
using Simpress.Product.Domain.Entities.DTO;
using Simpress.Product.Domain.Entities.Models;

namespace Simpress.Product.Application.AutoMapperConfig
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<Domain.Entities.Models.Product, ProductDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}
