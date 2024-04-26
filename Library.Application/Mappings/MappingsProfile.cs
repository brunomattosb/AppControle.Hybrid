using Library.Application.DTOs.EntitiesDTO;
using Shared.Entities;
using AutoMapper;

namespace Shared.DTO.Mappings;
public class MappingsProfile : Profile
{
    public MappingsProfile()
    {
        CreateMap<Product, ProductDTO>().ReverseMap();
        CreateMap<Product, ProductDTOCbb>().ReverseMap();
        CreateMap<Product, ProductDTOList>().ReverseMap();
        CreateMap<Category, CategoryDTO>().ReverseMap();
        CreateMap<City, CityDTO>().ReverseMap();
        CreateMap<State, StateDTO>().ReverseMap();
        CreateMap<Country, CountryDTO>().ReverseMap();
        CreateMap<Client, ClientDTO>().ReverseMap();
        CreateMap<User, UserDTO>().ReverseMap();
    }
}
