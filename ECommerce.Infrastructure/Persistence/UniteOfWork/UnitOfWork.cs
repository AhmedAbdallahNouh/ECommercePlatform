using ECommerce.Application.Common.Interfaces;
using ECommerce.Domain.Models;
using ECommerce.Infrastructure.Persistence.DbContexts;
using ECommerce.Infrastructure.Persistence.Repostories;

namespace ECommerce.Infrastructure.Persistence.UniteOfWork
{
    public class UnitOfWork(WriteDbContext writeContext,ReadDbContext readContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _readRepositories = [];
        private readonly Dictionary<string, object> _writeRepositories = [];

        private readonly WriteDbContext writeContext = writeContext;
        private readonly ReadDbContext readContext = readContext;


        public async Task<int> SaveChangesAsync() => await writeContext.SaveChangesAsync();

        public void Dispose()
        {
            writeContext.Dispose();
            readContext.Dispose();
        }
       
           
        public IWriteRepository<T> WriteRepository<T>() where T : BaseEntity
        {
            var type = typeof(T).Name;

            if (!_writeRepositories.ContainsKey(type))
            {
                var repo = new WriteRepository<T>(writeContext);
                _writeRepositories.Add(type, repo);
            }

            return (IWriteRepository<T>)_writeRepositories[type];
        }

        public IReadRepository<T> ReadRepository<T>() where T : BaseEntity
        {
            var type = typeof(T).Name;

            if (!_readRepositories.ContainsKey(type))
            {
                var repo = new ReadRepository<T>(readContext);
                _readRepositories.Add(type, repo);
            }

            return (IReadRepository<T>)_readRepositories[type];
        }
    }
}
