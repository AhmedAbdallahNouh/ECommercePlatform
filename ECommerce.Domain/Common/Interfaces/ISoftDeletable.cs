namespace ECommerce.Domain.Common
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }
        DateTime? DeletedAt { get; set; }
        void SoftDelete();
        void Restore();
    }
}
