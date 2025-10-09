using FluentValidation;

namespace ECommerce.Application.Payments.Commands.CreatePayment
{
    internal class CreatePaymentValidator : AbstractValidator<CreatePaymentCommand>
    {
        public CreatePaymentValidator()
        {
            RuleFor(x => x.OrderId)
                .GreaterThan(0).WithMessage("OrderId is required");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than zero");

            RuleFor(x => x.Method)
                .NotEmpty().WithMessage("Payment method is required");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Payment status is required");
        }
    }
}
