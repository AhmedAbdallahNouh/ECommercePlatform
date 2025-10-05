using ECommerce.Application.Abstraction.Messaging;
using ECommerce.Application.Products.DTOs;

namespace ECommerce.Application.Products.Queries.GetAllProducts
{
    public sealed record GetAllProductsQuery() : IQuery<IReadOnlyList<ProductDto>>;
}
