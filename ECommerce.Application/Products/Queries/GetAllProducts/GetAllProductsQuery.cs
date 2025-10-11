using ECommerce.Application.Abstraction.Messaging;
using ECommerce.Application.Products.DTOs;

namespace ECommerce.Application.Products.Queries.GetAllProducts
{
    public sealed record GetAllProductsQuery(
       string? SearchTerm,
       int? CategoryId,
       int PageNumber = 1,
       int PageSize = 10
   ) : IQuery<IReadOnlyList<ProductDto>>;
}
