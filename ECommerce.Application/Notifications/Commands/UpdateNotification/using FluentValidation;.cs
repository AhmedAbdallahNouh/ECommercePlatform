using FluentValidation;

namespace ECommerce.Application.Notifications.Commands.UpdateNotification
{
    internal class UpdateNotificationValidator : AbstractValidator<UpdateNotificationCommand>
    {
        public UpdateNotificationValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Message).NotEmpty();
            RuleFor(x => x.Type).NotEmpty();
        }
    }
}
