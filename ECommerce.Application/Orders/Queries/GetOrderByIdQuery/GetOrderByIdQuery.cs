using ECommerce.Application.Abstraction.Messaging;
using ECommerce.Application.Orders.DTOs;

namespace ECommerce.Application.Orders.Queries.GetOrderById
{
    public sealed record GetOrderByIdQuery(int Id) : IQuery<OrderDto>;
}
