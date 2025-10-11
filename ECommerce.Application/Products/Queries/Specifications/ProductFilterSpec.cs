using ECommerce.Application.Common.Specifications;
using ECommerce.Domain.Models;

namespace ECommerce.Application.Products.Queries.Specifications
{
    internal sealed class ProductFilterSpec : Specification<Product>
    {
        public ProductFilterSpec(string? searchTerm, int? categoryId , int skip = 0, int take = 10)
            : base(p => !p.IsDeleted &&
                        (string.IsNullOrEmpty(searchTerm) ||
                         p.Name.ToLower().Contains(searchTerm.ToLower()) ||
                         (p.Description != null && p.Description.ToLower().Contains(searchTerm.ToLower()))) &&
                        (!categoryId.HasValue || p.CategoryId == categoryId.Value))
        {
            AddInclude(p => p.Category);
            AddOrderBy(p => p.Name);
            ApplyPaging(skip ,take);
        }
    }
}
