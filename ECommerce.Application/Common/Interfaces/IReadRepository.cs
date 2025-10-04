using ECommerce.Domain.Models;

namespace ECommerce.Application.Common.Interfaces
{
    public interface IReadRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
    }
}
