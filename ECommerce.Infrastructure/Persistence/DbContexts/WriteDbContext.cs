using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Persistence.DbContexts
{
    public class WriteDbContext : ECommerceDbContext
    {
        public WriteDbContext(DbContextOptions<ECommerceDbContext> options)
            : base(options)
        {
        }
    }
}
