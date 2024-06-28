namespace Core.Models
{
    public class Transaction
    {
        public long Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public long CategoryId { get; set; }
        public string Title { get; set; } = string.Empty;
        public ETransactionType Type { get; set; } = ETransactionType.Withdraw;
        public decimal Amount { get; set; }
        public Category Category { get; set; } = null!;
        public DateTime? PaidOrReceivedAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}