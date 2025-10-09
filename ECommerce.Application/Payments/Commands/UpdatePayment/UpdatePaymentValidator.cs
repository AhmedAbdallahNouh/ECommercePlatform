using FluentValidation;

namespace ECommerce.Application.Payments.Commands.UpdatePayment
{
    internal class UpdatePaymentValidator : AbstractValidator<UpdatePaymentCommand>
    {
        public UpdatePaymentValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Payment ID is required");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than zero");

            RuleFor(x => x.Method)
                .NotEmpty().WithMessage("Payment method is required");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Payment status is required");
        }
    }
}
