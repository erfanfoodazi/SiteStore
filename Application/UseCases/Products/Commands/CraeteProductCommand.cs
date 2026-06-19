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
    public class CreateProductCommand : IRequest<CreateProductResultDto>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Stock { get; set; }
        public decimal Price { get; set; }
    }

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
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Stock = request.Stock,
                Price = request.Price,
                CreatedAt = DateTime.UtcNow
            };

            var result = await _productRepository.CreateProductAsync(product);

            if (result == null)
                throw new InvalidOperationException("Product creation failed");

            return new CreateProductResultDto
            {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description,
                Price = result.Price,
                Stock = result.Stock,
                CreatedAt = result.CreatedAt
            };
        }
    }
}
