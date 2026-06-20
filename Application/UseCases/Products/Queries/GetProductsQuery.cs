using Application.Common.Models;
using Application.UseCases.Products.DTOs;
using MediatR;

namespace Application.UseCases.Products.Queries
{
    public record GetProductsQuery(
    int Page,
    int PageSize,
    string? Search,
    int? CategoryId,
    decimal? MinPrice,
    decimal? MaxPrice
) : IRequest<PagedResult<ProductResultDto>>;

    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, PagedResult<ProductResultDto>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<PagedResult<ProductResultDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var (products, totalCount) = await _productRepository.GetProductsAsync(request.Page, request.PageSize, request.Search, request.CategoryId, request.MinPrice, request.MaxPrice, cancellationToken);

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
