using ECommerce.Application.Common.Specifications;
using ECommerce.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Persistence.Specifications
{
    public static class SpecificationQueryBuilder
    {
        public static IQueryable<T> ApplySpecification<T>(
            IQueryable<T> query, ISpecification<T> spec) where T : BaseEntity
        {
            query = query.AsNoTracking();

            if (spec.Criteria != null)
                query = query.Where(spec.Criteria);

            if (spec.Includes != null)
                query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            if (spec.OrderBy != null)
                query = query.OrderBy(spec.OrderBy);
            else if (spec.OrderByDesc != null)
                query = query.OrderByDescending(spec.OrderByDesc);

            if (spec.IsPagingEnabled)
                query = query.Skip(spec.Skip).Take(spec.Take);

            return query;
        }
    }
}
