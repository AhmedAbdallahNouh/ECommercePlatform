using ECommerce.Application.Abstraction.Messaging;

namespace ECommerce.Application.Products.Commands.DeleteProductCommand
{
    public sealed record DeactivateProductCommand(int Id) : ICommand;
}
