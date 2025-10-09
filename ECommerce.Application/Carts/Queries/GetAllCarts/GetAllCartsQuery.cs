using ECommerce.Application.Abstraction.Messaging;
using ECommerce.Application.Carts.DTOs;

namespace ECommerce.Application.Carts.Queries.GetAllCarts
{
    public sealed record GetAllCartsQuery() : IQuery<IReadOnlyList<CartDto>>;
}
