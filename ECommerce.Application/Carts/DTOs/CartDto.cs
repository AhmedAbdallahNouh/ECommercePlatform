namespace ECommerce.Application.Carts.DTOs
{
    public sealed record CartItemDto(
        int ProductId,
        int Quantity,
        decimal Price
    );
    public sealed record CartDto(
        int Id,
        string UserId,
        decimal TotalAmount,
        IReadOnlyList<CartItemDto> Items
    );
}
