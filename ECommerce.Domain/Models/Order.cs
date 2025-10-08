namespace ECommerce.Domain.Models
{
    public class Order : BaseEntity
    {

        // FK to Identity User
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;

        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "New";
        public DateTime Date { get; set; } = DateTime.UtcNow;

        // Navigation
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public Payment? Payment { get; set; }
    }

}
