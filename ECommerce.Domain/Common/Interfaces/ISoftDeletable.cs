﻿namespace ECommerce.Domain.Common
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; }
        DateTime? DeletedAt { get; }
        void SoftDelete();
        void Restore();
    }
}
