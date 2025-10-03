using ECommerce.Application.Common.Interfaces;
using ECommerce.Domain.Models;
using ECommerce.Infrastructure.Persistence.DbContext;
using ECommerce.Infrastructure.Persistence.Repostories;

namespace ECommerce.Infrastructure.Persistence.UniteOfWork
{
    public class UnitOfWork(ECommerceDbContext _context) : IUnitOfWork
    {
        private readonly ECommerceDbContext context = _context;
        private readonly Dictionary<string, object> repositories = [];

        public IRepository<T> Repository<T>() where T : BaseEntity
        {
            var type = typeof(T).Name;

            if(! repositories.ContainsKey(type))
            {
                var repo = new Repository<T>(context);
                repositories.Add(type, repo);
            }

            return (IRepository<T>)repositories[type];
        }

        public async Task<int> SaveChangesAsync() => await context.SaveChangesAsync();
 
        public void Dispose() => context.Dispose();
    }
}
