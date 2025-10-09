using ECommerce.Application.Abstraction.Messaging;

namespace ECommerce.Application.Notifications.Commands.UpdateNotification
{
    public sealed record UpdateNotificationCommand(
        int Id,
        string Message,
        string Type,
        bool IsRead
    ) : ICommand;
}
