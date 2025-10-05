using FluentValidation;

namespace ECommerce.Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Invalid category ID.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100);

            //RuleFor(x => x.Description)
            //    .MaximumLength(500);
        }
    }
}
