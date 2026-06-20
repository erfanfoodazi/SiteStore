using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Products.Commands
{
    public record DeleteProductImageCommand
    (
        int ImageId
        ) : IRequest<Unit>;


    public class DeleteProductImageCommandHandler : IRequestHandler<DeleteProductImageCommand, Unit>
    {
        private readonly IProductRepository _productRepository;
        public DeleteProductImageCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Unit> Handle(DeleteProductImageCommand request, CancellationToken cancellationToken)
        {
            await _productRepository.DeleteProductImageAsync(request.ImageId, cancellationToken);
            return Unit.Value;
        }
    }
}
