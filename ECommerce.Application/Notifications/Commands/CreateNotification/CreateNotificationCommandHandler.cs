using ECommerce.Application.Abstraction.Messaging;
using ECommerce.Application.Common.Interfaces;
using ECommerce.Domain.Models;
using ECommerce.Domain.Shared;

namespace ECommerce.Application.Notifications.Commands.CreateNotification
{
    internal sealed class CreateNotificationCommandHandler : ICommandHandler<CreateNotificationCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateNotificationCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = new Notification
            {
                UserId = request.UserId,
                Message = request.Message,
                Type = request.Type,
                Date = DateTime.UtcNow,
                IsRead = false
            };

            await _unitOfWork.WriteRepository<Notification>().AddAsync(notification);
            var result = await _unitOfWork.SaveChangesAsync();

            return result > 0 ? Result.Success(notification.Id) : Result.Failure<int>(Error.Failure);
        }
    }
}
