using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Stock { get; set; }
        public List<Rating> Ratings { get; set; } = new List<Rating>();
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public List<CategoryToProduct> Categories { get; set; } = new List<CategoryToProduct>();
        public decimal Price { get; set; }
        public int SellerId { get; set; }
        public User Seller { get; set; } = null!;
        public List<ProductImage> Images { get; set; } = new();
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    
}
