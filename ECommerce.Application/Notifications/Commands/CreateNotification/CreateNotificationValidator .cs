using FluentValidation;

namespace ECommerce.Application.Notifications.Commands.CreateNotification
{
    internal class CreateNotificationValidator : AbstractValidator<CreateNotificationCommand>
    {
        public CreateNotificationValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Message).NotEmpty().MaximumLength(500);
            RuleFor(x => x.Type).NotEmpty().MaximumLength(100);
        }
    }
}
