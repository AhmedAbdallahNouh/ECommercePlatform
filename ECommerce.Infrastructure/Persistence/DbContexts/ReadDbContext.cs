using ECommerce.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Persistence.DbContexts
{
    public class ReadDbContext : DbContext
    {
        public ReadDbContext(DbContextOptions<ReadDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<ApplicationUser> Users => Set<ApplicationUser>();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().ToTable("AspNetUsers");
        
            // -------------------------
            // User - Order (1..*)
            // -------------------------
            builder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

        }


    }
}
