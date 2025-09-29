namespace ECommerce.Domain.Models
{
    public class Cart : BaseEntity
    {
        // FK to Identity User
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;

        // Navigation
        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
    }

}
