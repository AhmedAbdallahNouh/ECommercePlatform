using ECommerce.Application.Abstraction.Messaging;
using ECommerce.Application.Carts.DTOs;

namespace ECommerce.Application.Carts.Commands.UpdateCart
{
    public sealed record UpdateCartCommand(
        int Id,
        IReadOnlyList<CartItemDto> Items
    ) : ICommand;
}
