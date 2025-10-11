using ECommerce.Application.Abstraction.Messaging;

namespace ECommerce.Application.Products.Commands.AdjustStock
{
    public sealed record AdjustStockCommand(int ProductId, int QuantityChange) : ICommand;
}
