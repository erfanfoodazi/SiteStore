using Application.UseCases.Products.DTOs;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Products.Commands
{
    public record CreateProductCommand(
    string Name,
    string Description,
    int Stock,
    decimal Price,
    int SellerId,
    List<string> ImageUrls
) : IRequest<CreateProductResultDto>;

    public class CreateProductCommandHandler
     : IRequestHandler<CreateProductCommand, CreateProductResultDto>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<CreateProductResultDto> Handle(
            CreateProductCommand request,
            CancellationToken cancellationToken)
        {

            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ArgumentException("Product name is required");

            if (request.Price <= 0)
                throw new ArgumentException("Price must be greater than zero");

            if (request.Stock < 0)
                throw new ArgumentException("Stock cannot be negative");

            if (request.ImageUrls.Any(string.IsNullOrWhiteSpace))
                throw new ArgumentException("Invalid image url");

            if (request.SellerId <= 0)
                throw new ArgumentException("Invalid seller id");

            var now = DateTime.UtcNow;
            var sku = $"PRD-{Guid.NewGuid():N}".ToUpper()[..16];

            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                SKU = sku,
                Stock = request.Stock,
                Price = request.Price,
                SellerId = request.SellerId,
                CreatedAt = now,
                UpdatedAt = now,
                Images = request.ImageUrls
                    .Select(url => new ProductImage
                    {
                        Url = url,
                        CreatedAt = now
                    })
                    .ToList()
            };


            var result = await _productRepository.CreateProductAsync(product, cancellationToken);

            if (result == null)
                throw new InvalidOperationException("Product creation failed");


            var resultImages = result.Images
                .Select(image => new ProductImageResultDto
                {
                    Id = image.Id,
                    ProductId = image.ProductId,
                    Url = image.Url,
                    CreatedAt = image.CreatedAt
                })
                .ToList();


            return new CreateProductResultDto
            {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description,
                Price = result.Price,
                Stock = result.Stock,
                CreatedAt = result.CreatedAt,
                SKU = result.SKU,
                Images = resultImages,
            };
        }
    }
}
