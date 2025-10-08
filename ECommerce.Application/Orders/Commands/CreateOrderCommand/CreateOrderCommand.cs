using ECommerce.Application.Abstraction.Messaging;

namespace ECommerce.Application.Orders.Commands.CreateOrder
{
    public sealed record CreateOrderItemDto(int ProductId, int Quantity, decimal Price);

    public sealed record CreateOrderCommand(
        string UserId,
        decimal TotalAmount,
        string Status,
        List<CreateOrderItemDto> Items
    ) : ICommand<int>;
}
