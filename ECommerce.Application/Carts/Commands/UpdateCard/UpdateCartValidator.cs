using FluentValidation;

namespace ECommerce.Application.Carts.Commands.UpdateCart
{
    internal class UpdateCartValidator : AbstractValidator<UpdateCartCommand>
    {
        public UpdateCartValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Items).NotEmpty().WithMessage("Cart must contain at least one item.");
        }
    }
}
