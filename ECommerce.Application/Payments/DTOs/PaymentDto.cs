namespace ECommerce.Application.Payments.DTOs
{
    public sealed record PaymentDto(
        int Id,
        int OrderId,
        decimal Amount,
        string Method,
        string Status,
        DateTime? createdAt,
        DateTime? updatedAt
    );  
}
