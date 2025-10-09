namespace ECommerce.Application.Carts.DTOs
{
    public sealed record CartItemDto(
        int ProductId,
        int Quantity
    );

    public sealed record CartDto(
        int Id,
        string UserId,
        decimal TotalAmount,
        IReadOnlyList<CartItemDto> Items
    );
}
