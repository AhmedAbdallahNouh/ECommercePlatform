using ECommerce.Domain.Common;

namespace ECommerce.Domain.Models
{
    public class Product : BaseEntity , ISoftDeletable
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int SoldCount { get; set; } = 0;
        public void AdjustStock(int quantity)
        {
            if (Stock + quantity < 0)
                throw new InvalidOperationException("Stock cannot be negative.");
            Stock += quantity;
        }

        public void SoftDelete()
        {
            IsDeleted = true;
            DeletedAt = DateTime.UtcNow;
        }

        public void Restore()
        {
            IsDeleted = false;
            DeletedAt = null;
        }

        // FK
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public bool IsDeleted { get; private set; } = false;
        public DateTime? DeletedAt { get; private set; }
    }

}
