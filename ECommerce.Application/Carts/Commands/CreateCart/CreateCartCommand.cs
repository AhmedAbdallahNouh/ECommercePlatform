using ECommerce.Application.Abstraction.Messaging;
using ECommerce.Application.Carts.DTOs;

namespace ECommerce.Application.Carts.Commands.CreateCart
{
    public sealed record CreateCartCommand(
        string UserId,
        IReadOnlyList<CartItemDto> Items
    ) : ICommand<int>;
}
