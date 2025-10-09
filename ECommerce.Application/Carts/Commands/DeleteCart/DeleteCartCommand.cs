using ECommerce.Application.Abstraction.Messaging;

namespace ECommerce.Application.Carts.Commands.DeleteCart
{
    public sealed record DeleteCartCommand(int Id) : ICommand;
}
