using FluentValidation;

namespace ECommerce.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.UserId)
              .NotEmpty().WithMessage("UserId is required.");

            RuleFor(x => x.TotalAmount).GreaterThan(0);
            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required.");
            RuleForEach(x => x.Items)
                .SetValidator(new CreateOrderItemValidator());
        }
    }

    public class CreateOrderItemValidator : AbstractValidator<CreateOrderItemDto>
    {
        public CreateOrderItemValidator()
        {
            RuleFor(x => x.ProductId).GreaterThan(0);
            RuleFor(x => x.Quantity).GreaterThan(0);
            RuleFor(x => x.Price).GreaterThan(0);
        }
    }
}
