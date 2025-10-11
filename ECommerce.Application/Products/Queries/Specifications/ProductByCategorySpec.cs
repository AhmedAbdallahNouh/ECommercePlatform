using ECommerce.Application.Common.Specifications;
using ECommerce.Domain.Models;


namespace ECommerce.Application.Products.Queries.Specifications
{
    public class ProductByCategorySpec : Specification<Product>
    {
        public ProductByCategorySpec(int categoryId) : base(product => product.CategoryId == categoryId && product.IsDeleted == false)
        {
            AddInclude(Product => Product.Category);
            AddOrderBy(product => product.Name);
        }
    }
}
        