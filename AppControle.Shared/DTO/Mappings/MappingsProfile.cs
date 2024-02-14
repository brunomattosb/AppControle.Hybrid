using AppControle.Shared.DTO.EntitiesDTO;
using AppControle.Shared.Entities;
using AutoMapper;

namespace AppControle.Shared.DTO.Mappings;
public class MappingsProfile : Profile
{
    public MappingsProfile()
    {
        CreateMap<Product, ProductDTO>().ReverseMap();
        CreateMap<Product, ProductDTOCbb>().ReverseMap();
        CreateMap<Product, ProductDTOList>().ReverseMap();
        CreateMap<Category, CategoryDTO>().ReverseMap();
    }
}
