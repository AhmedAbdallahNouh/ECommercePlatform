namespace ECommerce.Domain.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        // Navigation
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
