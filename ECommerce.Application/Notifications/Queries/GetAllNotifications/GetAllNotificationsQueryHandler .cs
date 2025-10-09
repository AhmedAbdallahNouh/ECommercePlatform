using ECommerce.Application.Abstraction.Messaging;
using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Notifications.DTOs;
using ECommerce.Domain.Models;
using ECommerce.Domain.Shared;

namespace ECommerce.Application.Notifications.Queries.GetAllNotifications
{
    internal sealed class GetAllNotificationsQueryHandler : IQueryHandler<GetAllNotificationsQuery, IReadOnlyList<NotificationDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllNotificationsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IReadOnlyList<NotificationDto>>> Handle(GetAllNotificationsQuery request, CancellationToken cancellationToken)
        {
            var notifications = await _unitOfWork.ReadRepository<Notification>().GetAllAsync();

            var dtos = notifications.Select(n => new NotificationDto(
                n.Id,
                n.UserId,
                n.Message,
                n.Type,
                n.Date,
                n.IsRead
            )).ToList();

            return Result.Success((IReadOnlyList<NotificationDto>)dtos);
        }
    }
}
