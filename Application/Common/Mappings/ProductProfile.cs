using AutoMapper;
using Domain.Entities;
using Application.UseCases.Products.DTOs;

namespace Application.Common.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, CreateProductResultDto>();

            CreateMap<ProductImage, ProductImageResultDto>();
        }
    }
}