using ECommerce.Application.Abstraction.Messaging;
using ECommerce.Application.Common.Interfaces;
using ECommerce.Domain.Models;
using ECommerce.Domain.Shared;

namespace ECommerce.Application.Notifications.Commands.DeleteNotification
{
    internal sealed class DeleteNotificationCommandHandler : ICommandHandler<DeleteNotificationCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteNotificationCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = await _unitOfWork.ReadRepository<Notification>().GetByIdAsync(request.Id);
            if (notification is null)
                return Result.Failure(Error.NullValue);

            _unitOfWork.WriteRepository<Notification>().Delete(notification);
            var result = await _unitOfWork.SaveChangesAsync();

            return result > 0 ? Result.Success() : Result.Failure(Error.Failure);
        }
    }
}
