using AutoMapper;
using Catalog.Application.Commands;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Specs;

namespace Catalog.Application.Mappers
{
    internal class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<ProductBrand, BrandResponse>().ReverseMap();

            CreateMap<ProductType, TypeResponse>().ReverseMap();

            CreateMap<Product, ProductResponse>().ReverseMap();

            CreateMap<Pagination<Product>, Pagination<ProductResponse>>().ReverseMap();

            CreateMap<Product, CreateProductCommand>().ReverseMap();
        }
    }
}
