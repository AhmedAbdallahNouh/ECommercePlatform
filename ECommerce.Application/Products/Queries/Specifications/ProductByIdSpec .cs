using ECommerce.Application.Common.Specifications;
using ECommerce.Domain.Models;

namespace ECommerce.Application.Products.Queries.Specifications
{
    internal sealed class ProductByIdSpec : Specification<Product>
    {
        public ProductByIdSpec(int id)
            : base(p => p.Id == id)
        {
            AddInclude(p => p.Category);
            AddOrderBy(p => p.Name);
        }
    }

}
