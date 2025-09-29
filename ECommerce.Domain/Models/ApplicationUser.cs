using Microsoft.AspNetCore.Identity;

namespace ECommerce.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Extra profile fields if needed
        public string? FullName { get; set; }

        // Navigation
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<Cart> Carts { get; set; } = new List<Cart>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
