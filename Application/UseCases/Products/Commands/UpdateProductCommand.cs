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
    public record UpdateProductCommand(
        int Id,
        string Name,
        string Description,
        int Stock,
        decimal Price
        ) : IRequest<UpdateProductResultDto>;


    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdateProductResultDto>
    {
        private readonly IProductRepository _productRepository;
        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<UpdateProductResultDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByIdAsync(request.Id, cancellationToken);

            if (product == null)
                throw new ArgumentException("Product not found");

            product.Name = request.Name;
            product.Description = request.Description;
            product.Stock = request.Stock;
            product.Price = request.Price;
            product.UpdatedAt = DateTime.UtcNow;

            var result = await _productRepository.UpdateProductAsync(product, cancellationToken);
            if (!result)
                throw new ArgumentException("product update failed");

            return new UpdateProductResultDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                UpdatedAt = product.UpdatedAt,
                SKU = product.SKU,
                Images = product.Images.Select(image => new ProductImageResultDto
                {
                    Id = image.Id,
                    ProductId = image.ProductId,
                    Url = image.Url,
                    CreatedAt = image.CreatedAt
                }).ToList()

            };
        }
    }
}
