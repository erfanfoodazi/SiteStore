using System.Transactions;

namespace Domain.Entities
{
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
}
