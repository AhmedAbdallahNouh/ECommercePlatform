using ECommerce.Application.Abstraction.Messaging;

namespace ECommerce.Application.Notifications.Commands.DeleteNotification
{
    public sealed record DeleteNotificationCommand(int Id) : ICommand;
}
