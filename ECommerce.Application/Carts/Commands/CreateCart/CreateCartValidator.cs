using FluentValidation;

namespace ECommerce.Application.Carts.Commands.CreateCart
{
    internal class CreateCartValidator : AbstractValidator<CreateCartCommand>
    {
        public CreateCartValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");
            RuleFor(x => x.Items).NotEmpty().WithMessage("Cart must have at least one item.");
            RuleForEach(x => x.Items).ChildRules(item =>
            {
                item.RuleFor(i => i.ProductId).GreaterThan(0);
                item.RuleFor(i => i.Quantity).GreaterThan(0);
                item.RuleFor(i => i.Price).GreaterThan(0);
            });
        }
    }
}
