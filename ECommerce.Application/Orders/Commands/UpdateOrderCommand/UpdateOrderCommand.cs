using ECommerce.Application.Abstraction.Messaging;

namespace ECommerce.Application.Orders.Commands.UpdateOrder
{
    public sealed record UpdateOrderCommand(int Id, string Status) : ICommand;
}
