using ECommerce.Application.Common.Interfaces;
using ECommerce.Domain.Models;
using ECommerce.Infrastructure.Persistence.DbContext;
using ECommerce.Infrastructure.Persistence.Repostories;

namespace ECommerce.Infrastructure.Persistence.UniteOfWork
{
    public class UnitOfWork(ECommerceDbContext _context , Dictionary<string, object> _repositories) : IUnitOfWork
    {  
        public IRepository<T> Repository<T>() where T : BaseEntity
        {
            var type = typeof(T).Name;

            if(! _repositories.ContainsKey(type))
            {
                var repo = new Repository<T>(_context);
                _repositories.Add(type, repo);
            }

            return (IRepository<T>)_repositories[type];
        }

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
 
        public void Dispose() => _context.Dispose();
    }
}
