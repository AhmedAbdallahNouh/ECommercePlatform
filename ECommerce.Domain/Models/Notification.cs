namespace ECommerce.Domain.Models
{
    public class Notification : BaseEntity
    {
        // FK to Identity User
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;

        public string Message { get; set; } = string.Empty;
        public string Type { get; set; } = "Email"; // or "SMS"
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public bool IsRead { get; set; }
    }

}
