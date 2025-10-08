namespace ECommerce.Application.Orders.DTOs
{
    public sealed record OrderItemDto(int ProductId, int Quantity, decimal Price);

    public sealed record OrderDto(
        int Id,
        string UserId,
        decimal TotalAmount,
        string Status,
        DateTime Date,
        List<OrderItemDto> Items
    );
}
