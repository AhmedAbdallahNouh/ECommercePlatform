using ECommerce.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ECommerce.Infrastructure.Interceptors
{
    public class SoftDeleteInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(
            DbContextEventData eventData,
            InterceptionResult<int> result)
        {
            if (eventData.Context is null)
                return result;

            // Find entities that are marked for deletion and implement ISoftDeletable
            var entriesToSoftDelete = eventData.Context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Deleted && e.Entity is ISoftDeletable)
                .ToList();

            foreach (var entry in entriesToSoftDelete)
            {
                var entity = (ISoftDeletable)entry.Entity;

                ((ISoftDeletable)entry.Entity).SoftDelete();

                // Change EF state from Deleted → Modified
                entry.State = EntityState.Modified;
            }

            return base.SavingChanges(eventData, result);
        }   
    }
}
