using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class User : IdentityUser<int>
    {
        public string FullName { get; set; } = string.Empty;
        public UserType UserType { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
        public List<Product> Products { get; set; } = new();
        public List<Rating> Ratings { get; set; } = new();
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public User() { }

        public User(string fullName, UserType userType, string email, string userName)
        {
            FullName = fullName;
            Email = email;
            UserName = userName;
            UserType = userType;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
