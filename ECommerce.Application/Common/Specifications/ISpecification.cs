    using ECommerce.Domain.Models;
using System.Linq.Expressions;

namespace ECommerce.Application.Common.Specifications
{
    public interface ISpecification<T> where T : BaseEntity
    {
        Expression<Func<T, bool>>? Criteria { get; }
        List<Expression<Func<T, object>>>? Includes { get; }
        Expression<Func<T, object>>? OrderBy{get;}
        Expression<Func<T, object>>? OrderByDesc{get;}
        bool IsPagingEnabled { get; }
        int Skip { get; }
        int Take { get; }
    }
}
