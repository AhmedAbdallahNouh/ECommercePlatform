using ECommerce.Application.Abstraction.Messaging;

namespace ECommerce.Application.Products.Commands.UpdateProductCommand
{
    public sealed record UpdateProductCommand(
         int Id,
         string Name,
         string? Description,
         decimal Price,
         int Stock,
         bool IsFeatured
     ) : ICommand<int>;
}
