using Application.UseCases.Products.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Application.UseCases.Products.Queries
{
    public record GetProductByIdQuery
    (
        int Id
        ) : IRequest<ProductResultDto>;


    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductResultDto>
    {
        private readonly IProductRepository _productRepository;
        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductResultDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByIdAsync( request.Id ,cancellationToken);

            if (product == null)
                throw new KeyNotFoundException("Product not found");

            return new ProductResultDto()
            {
                Id = product.Id,
                Name = product.Name,
                SKU = product.SKU,
                Description = product.Description,
                Price = product.Price,
                SellerId = product.SellerId,
                Stock = product.Stock,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt,
                Images = product.Images?
                .Select(image => new ProductImageResultDto
                {
                    Id = image.Id,
                    ProductId = image.ProductId,
                    Url = image.Url,
                    CreatedAt = image.CreatedAt
                })
                .ToList() ?? new(),

                Ratings = product.Ratings?
                .Select(rating => new RatingResultDto
                {
                    Id = rating.Id,
                    CreatedAt = rating.CreatedAt,
                    Comment = rating.Comment,
                    Score = rating.Score,
                    UserId = rating.UserId,
                }).ToList() ?? new(),
            };
        }
    }
}
