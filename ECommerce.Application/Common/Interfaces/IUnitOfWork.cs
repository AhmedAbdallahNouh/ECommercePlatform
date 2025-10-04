using ECommerce.Domain.Models;

namespace ECommerce.Application.Common.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IWriteRepository<T> WriteRepository<T>() where T : BaseEntity;
        IReadRepository<T> ReadRepository<T>() where T : BaseEntity;
        Task<int> SaveChangesAsync();
    }
}
