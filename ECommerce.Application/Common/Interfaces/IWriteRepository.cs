using ECommerce.Domain.Models;

namespace ECommerce.Application.Common.Interfaces
{
    public interface IWriteRepository<T> where T : BaseEntity
    {
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
