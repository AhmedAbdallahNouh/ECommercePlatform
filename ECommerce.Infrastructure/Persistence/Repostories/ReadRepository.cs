using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Common.Specifications;
using ECommerce.Domain.Models;
using ECommerce.Infrastructure.Persistence.DbContexts;
using ECommerce.Infrastructure.Persistence.Specifications;
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


        public async Task<T?> GetEntityWithSpecAsync(ISpecification<T> spec)
        {
            var query = SpecificationQueryBuilder.ApplySpecification(_dbSet, spec);
            return await query.FirstOrDefaultAsync();
        }

    }

}
