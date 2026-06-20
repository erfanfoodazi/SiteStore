using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Products.Commands
{
    public record AddProductImageCommand
    (
        int ProductId,
        string Url
        ) : IRequest<Unit>;


    public class AddProductImageCommandHandler : IRequestHandler<AddProductImageCommand, Unit>
    {
        private readonly IProductRepository _productRepository;
        public AddProductImageCommandHandler( IProductRepository productRepository )
        {
            _productRepository = productRepository;
        }
        public async Task<Unit> Handle(AddProductImageCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByIdAsync( request.ProductId, cancellationToken );

            if (product == null)
                throw new KeyNotFoundException("Product not found");

            if (string.IsNullOrWhiteSpace(request.Url))
                throw new ArgumentException("Image url is required");

            var image = new ProductImage
            {
                Url = request.Url,
                CreatedAt = DateTime.UtcNow,
            };

            await _productRepository.AddImageToProduct( product.Id, image, cancellationToken );

            return Unit.Value;
        }
    }
}
