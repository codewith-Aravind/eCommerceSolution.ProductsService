using AutoMapper;
using eCommerce.BussinessLogicLayer.DTO;
using eCommerce.DataAccessLayer.Entities;

namespace eCommerce.BussinessLogicLayer.Mappers;

public class ProductToProductMappingProfile : Profile
{
    public ProductToProductMappingProfile()
    {
        CreateMap<Product, ProductResponse>()
  .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
  .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
  .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
  .ForMember(dest => dest.QuantityInStock, opt => opt.MapFrom(src => src.QuantityInStock))
  .ForMember(dest => dest.ProductID, opt => opt.MapFrom(src => src.ProductID))
  ;
    }
}
