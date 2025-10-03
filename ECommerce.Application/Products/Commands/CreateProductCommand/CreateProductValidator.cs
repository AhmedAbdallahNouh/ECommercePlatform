using FluentValidation;


namespace ECommerce.Application.Products.Commands.CreateProductCommand
{
    internal class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(200).WithMessage("Maximum length is 200 characters");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero");

            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("Stock cannot be negative");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("CategoryId must be specified");

        }
    }
}
