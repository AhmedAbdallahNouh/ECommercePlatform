using ECommerce.Domain.Models;
using System.Linq.Expressions;

namespace ECommerce.Application.Common.Specifications
{
    public abstract class Specification<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>>? Criteria { get; private set; }
        public List<Expression<Func<T, object>>>? Includes { get; } = new();

        public Expression<Func<T, object>>? OrderBy { get; private set; }

        public Expression<Func<T, object>>? OrderByDesc { get; private set; }

        public bool IsPagingEnabled { get; private set; } = false;

        public int Skip { get; private set; } = 0;

        public int Take { get; private set; } = 0;

        protected Specification(Expression<Func<T , bool>> criteria) 
        {
            Criteria = criteria;
        }

        protected void AddInclude(Expression<Func<T, object>> includeExpression) 
        {
            Includes?.Add(includeExpression);
        }

        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression) 
        {
           OrderBy = orderByExpression;
        }
        protected void AddOrderByDesc(Expression<Func<T, object>> orderByExpression) 
        {
           OrderByDesc = orderByExpression;
        }

        protected void ApplyPaging(int skip, int take) 
        {
            IsPagingEnabled = true;
            Skip = skip;
            Take = take;
        }

    }
}
