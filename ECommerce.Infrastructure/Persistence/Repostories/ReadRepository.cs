using ECommerce.Application.Common.Interfaces;
using ECommerce.Domain.Models;
using ECommerce.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Persistence.Repostories
{
    public class ReadRepository<T>(ReadDbContext context) : IReadRepository<T> where T : BaseEntity
    {
        private readonly DbSet<T> _dbSet = context.Set<T>();

        public async Task<T?> GetByIdAsync(int id) =>
            await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);

        public async Task<IReadOnlyList<T>> GetAllAsync() =>
            await _dbSet.AsNoTracking().ToListAsync();
    }

}
