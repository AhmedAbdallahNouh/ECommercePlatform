namespace ECommerce.Domain.Models
{
    public class Review
    {
        public int ReviewId { get; set; }

        // FKs
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public int Rating { get; set; } // 1-5
        public string? Comment { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }

}
