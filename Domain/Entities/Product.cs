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
        public decimal AverageRating { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public List<CategoryToProduct> Categories { get; set; } = new List<CategoryToProduct>();
        public decimal Price { get; set; }
        public int SellerId { get; set; }
        public User Seller { get; set; } = null!;
        public List<ProductImage> Images { get; set; } = new();
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class ProductImage
    {
        public int Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
    public class Rating
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Score { get; set; }
        public string? Comment { get; set; }
        public User? User { get; set; }
        public Product? Product { get; set; }
        public DateTime CreatedAt { get; set; }

    }


    public class OrderItem
    {
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int OrderId { get; set; }
        public Order? Order { get; set; }


    }

    public class Order
    {
        public int Id { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public int UserId { get; set; }
        public User? User { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public enum OrderStatus
    {
        Pending,
        Paid,
        Processing,
        Shipped,
        Delivered,
        Cancelled
    }

    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public UserType UserType { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
        public List<Product> Products { get; set; } = new();
        public List<Rating> Ratings { get; set; } = new();
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


    }

    public enum UserType
    {
        Customer,
        Seller,
        Admin
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<CategoryToProduct> CategoryProducts { get; set; } = new List<CategoryToProduct>();
    }

    public class CategoryToProduct
    {
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }

    public class Transaction
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public int OrderId { get; set; }
        public Order? Order { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public TransactionStatus Status { get; set; }

    }

    public enum TransactionStatus
    {
        Pending,
        Successful,
        Failed,
        Refunded
    }
}
