namespace ECommerce.Domain.Models
{
    public class Payment : BaseEntity
    {
        // FK
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public decimal Amount { get; set; }
        public string Method { get; set; } = string.Empty; // e.g. "CreditCard", "PayPal"
        public string Status { get; set; } = "Pending";
    }

}
