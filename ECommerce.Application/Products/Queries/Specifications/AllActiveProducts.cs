using ECommerce.Application.Common.Specifications;
using ECommerce.Domain.Models;

namespace ECommerce.Application.Products.Queries.Specifications
{
    public sealed class ProductSpecifications
    {
        public static Specification<Product> AllActiveProducts(string? searchTerm, int? categoryId , int skip = 0, int take = 10)
        {
            return new ProductFilterSpec(searchTerm, categoryId, skip, take);
        }
    }
}
