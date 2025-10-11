
using ECommerce.Application.Abstraction.Messaging;

namespace ECommerce.Application.Products.Commands.CreateProductCommand
{
    public sealed record CreateProductCommand(
       string Name,
       string? Description,
       decimal Price,
       int Stock,
       int CategoryId,
       bool IsFeatured = false
   ) : ICommand<int>;
}
