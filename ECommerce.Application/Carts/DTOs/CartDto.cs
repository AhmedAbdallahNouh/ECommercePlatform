namespace ECommerce.Application.Carts.DTOs
{
    public sealed record CartItemDto(
        int ProductId,
        int Quantity,
        int Price
    );
    public sealed record CartDto(
        int Id,
        string UserId,
        decimal TotalAmount,
        IReadOnlyList<CartItemDto> Items
    );
}
