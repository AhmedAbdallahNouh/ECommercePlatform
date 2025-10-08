using ECommerce.Application.Abstraction.Messaging;

namespace ECommerce.Application.Orders.Commands.DeleteOrder
{
    public sealed record DeleteOrderCommand(int Id) : ICommand;
}
