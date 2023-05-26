using AutoMapper;
using Simpress.Product.Domain.Entities.DTO;
using Simpress.Product.Domain.Entities.Models;

namespace Simpress.Product.API.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Domain.Entities.Models.Product, ProductDto>().ReverseMap();
            CreateMap<Category,CategoryDto>().ReverseMap(); 
        }
    }
}
