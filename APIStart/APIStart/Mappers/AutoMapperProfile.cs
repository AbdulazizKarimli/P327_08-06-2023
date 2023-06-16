using APIStart.DTOs.ProductDtos;
using APIStart.Models;
using AutoMapper;

namespace APIStart.Mappers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Product, ProductGetResponseDto>().ReverseMap();
        CreateMap<ProductCreateDto, Product>().ReverseMap();
        CreateMap<ProductUpdateDto, Product>().ReverseMap();
    }
}