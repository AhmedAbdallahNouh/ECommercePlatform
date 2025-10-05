using FluentValidation;

namespace ECommerce.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100);

            //RuleFor(x => x.Description)
            //    .MaximumLength(500);
        }
    }
}
