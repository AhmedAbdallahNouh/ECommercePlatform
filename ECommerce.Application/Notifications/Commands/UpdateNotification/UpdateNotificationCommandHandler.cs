using ECommerce.Application.Abstraction.Messaging;
using ECommerce.Application.Common.Interfaces;
using ECommerce.Domain.Models;
using ECommerce.Domain.Shared;

namespace ECommerce.Application.Notifications.Commands.UpdateNotification
{
    internal sealed class UpdateNotificationCommandHandler : ICommandHandler<UpdateNotificationCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateNotificationCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = await _unitOfWork.ReadRepository<Notification>().GetByIdAsync(request.Id);
            if (notification is null)
                return Result.Failure(Error.NullValue);

            notification.Message = request.Message;
            notification.Type = request.Type;
            notification.IsRead = request.IsRead;

            _unitOfWork.WriteRepository<Notification>().Update(notification);
            var result = await _unitOfWork.SaveChangesAsync();

            return result > 0 ? Result.Success() : Result.Failure(Error.Failure);
        }
    }
}
