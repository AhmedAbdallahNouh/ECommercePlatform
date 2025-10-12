using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ECommerce.Infrastructure.Persistence.DbContexts
{
    public class WriteDbContextFactory : IDesignTimeDbContextFactory<WriteDbContext>
    {
        public WriteDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WriteDbContext>();

            // 👇 خلي connection string هنا زي اللي في appsettings.json
            optionsBuilder.UseSqlServer("Server=.;Database=ECommerce_WriteDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");

            return new WriteDbContext(optionsBuilder.Options);
        }
    }
}
