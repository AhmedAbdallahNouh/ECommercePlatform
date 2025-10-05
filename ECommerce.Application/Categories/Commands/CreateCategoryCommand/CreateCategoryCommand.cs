using ECommerce.Application.Abstraction.Messaging;

namespace ECommerce.Application.Categories.Commands.CreateCategory
{
    public sealed record CreateCategoryCommand(string Name) : ICommand<int>;
}
