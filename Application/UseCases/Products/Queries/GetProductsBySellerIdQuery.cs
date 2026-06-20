using Application.Common.Models;
using Application.UseCases.Products.DTOs;
using MediatR;

namespace Application.UseCases.Products.Queries
{
    public record GetProductsBySellerIdQuery(
        int SellerId,
        int Page,
        int PageSize,
        string? Search,
        int? CategoryId,
        decimal? MinPrice,
        decimal? MaxPrice
    ) : IRequest<PagedResult<ProductResultDto>>;

    public class GetProductsBySellerIdQueryHandler : IRequestHandler<GetProductsBySellerIdQuery, PagedResult<ProductResultDto>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductsBySellerIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<PagedResult<ProductResultDto>> Handle(GetProductsBySellerIdQuery request, CancellationToken cancellationToken)
        {
            var (products, totalCount) = await _productRepository.GetProductsBySellerIdAsync(request.SellerId, request.Page, request.PageSize, request.Search, request.CategoryId, request.MinPrice, request.MaxPrice, cancellationToken);

            var items = products.Select(product => new ProductResultDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                SKU = product.SKU,
                Price = product.Price,
                Stock = product.Stock,
                SellerId = product.SellerId,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt,
                Images = product.Images?.Select(image => new ProductImageResultDto
                {
                    Id = image.Id,
                    ProductId = image.ProductId,
                    Url = image.Url,
                    CreatedAt = image.CreatedAt
                }).ToList() ?? new(),
                Ratings = product.Ratings?.Select(rating => new RatingResultDto
                {
                    Id = rating.Id,
                    UserId = rating.UserId,
                    Score = rating.Score,
                    Comment = rating.Comment,
                    CreatedAt = rating.CreatedAt
                }).ToList() ?? new()
            }).ToList();

            return new PagedResult<ProductResultDto>
            {
                Items = items,
                TotalCount = totalCount,
                Page = request.Page,
                PageSize = request.PageSize
            };
        }
    }
}
