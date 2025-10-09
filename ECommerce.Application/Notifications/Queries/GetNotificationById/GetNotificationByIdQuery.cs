using ECommerce.Application.Abstraction.Messaging;
using ECommerce.Application.Notifications.DTOs;

namespace ECommerce.Application.Notifications.Queries.GetNotificationById
{
    public sealed record GetNotificationByIdQuery(int Id) : IQuery<NotificationDto>;
}
