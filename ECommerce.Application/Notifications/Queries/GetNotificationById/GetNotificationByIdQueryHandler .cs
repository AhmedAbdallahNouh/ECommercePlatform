using ECommerce.Application.Abstraction.Messaging;
using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Notifications.DTOs;
using ECommerce.Domain.Models;
using ECommerce.Domain.Shared;

namespace ECommerce.Application.Notifications.Queries.GetNotificationById
{
    internal sealed class GetNotificationByIdQueryHandler : IQueryHandler<GetNotificationByIdQuery, NotificationDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetNotificationByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<NotificationDto>> Handle(GetNotificationByIdQuery request, CancellationToken cancellationToken)
        {
            var notification = await _unitOfWork.ReadRepository<Notification>().GetByIdAsync(request.Id);
            if (notification is null)
                return Result.Failure<NotificationDto>(Error.NullValue);

            var dto = new NotificationDto(
                notification.Id,
                notification.UserId,
                notification.Message,
                notification.Type,
                notification.Date,
                notification.IsRead
            );

            return Result.Success(dto);
        }
    }
}
