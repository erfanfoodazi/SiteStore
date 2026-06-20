using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Products
{
    public interface IProductRepository
    {
        Task<Product> CreateProductAsync(Product product, CancellationToken cancellationToken = default);
        Task<bool> UpdateProductAsync(Product product, CancellationToken cancellationToken = default);
        Task DeleteProductAsync(int productId, CancellationToken cancellationToken = default);
        Task<Product> GetProductByIdAsync(int productId, CancellationToken cancellationToken = default);
        Task AddImageToProduct(int productId, ProductImage image, CancellationToken cancellationToken = default);
        Task DeleteProductImageAsync(int imageId, CancellationToken cancellationToken = default);
        Task<(List<Product> Items, int TotalCount)> GetProductsAsync(int page, int pageSize, string? search = null, int? categoryId = null, decimal? minPrice = null, decimal? maxPrice = null, CancellationToken cancellationToken = default);
        Task<(List<Product> Items, int TotalCount)> GetProductsBySellerIdAsync(int sellerId, int page, int pageSize, string? search = null, int? categoryId = null, decimal? minPrice = null, decimal? maxPrice = null, CancellationToken cancellationToken = default);
    }
}
