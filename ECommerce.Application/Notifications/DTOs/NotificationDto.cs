namespace ECommerce.Application.Notifications.DTOs
{
    public sealed record NotificationDto(
        int Id,
        string UserId,
        string Message,
        string Type,
        DateTime Date,
        bool IsRead
    );
}
