using ECommerce.Application.Abstraction.Messaging;
using ECommerce.Application.Carts.DTOs;

namespace ECommerce.Application.Carts.Queries.GetCartById
{
    public sealed record GetCartByIdQuery(int Id) : IQuery<CartDto>;
}
