using ECommerce.Application.Abstraction.Messaging;

namespace ECommerce.Application.Notifications.Commands.CreateNotification
{
    public sealed record CreateNotificationCommand(
        string UserId,
        string Message,
        string Type
    ) : ICommand<int>;
}
