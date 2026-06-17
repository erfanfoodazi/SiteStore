using Domain.Enums;

namespace Domain.Entities
{
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
}
