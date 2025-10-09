using ECommerce.Application.Abstraction.Messaging;
using ECommerce.Application.Notifications.DTOs;

namespace ECommerce.Application.Notifications.Queries.GetAllNotifications
{
    public sealed record GetAllNotificationsQuery() : IQuery<IReadOnlyList<NotificationDto>>;
}
