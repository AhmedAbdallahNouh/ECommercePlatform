using ECommerce.Application.Abstraction.Messaging;

namespace ECommerce.Application.Categories.Commands.UpdateCategory
{
    public sealed record UpdateCategoryCommand(int Id, string Name) : ICommand;
}
