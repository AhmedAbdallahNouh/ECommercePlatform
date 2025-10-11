using ECommerce.Application.Abstraction.Messaging;
using ECommerce.Application.Common.Interfaces;
using ECommerce.Domain.Models;
using ECommerce.Domain.Shared;

namespace ECommerce.Application.Products.Commands.AdjustStock
{
    internal sealed class AdjustStockCommandHandler(IUnitOfWork unitOfWork)
        : ICommandHandler<AdjustStockCommand>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result> Handle(AdjustStockCommand request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.ReadRepository<Product>().GetByIdAsync(request.ProductId);
            if (product is null)
                return Result.Failure(Error.NullValue);

            try
            {
                product.AdjustStock(request.QuantityChange);
            }
            catch (InvalidOperationException ex)
            {
                return Result.Failure(new Error("Product.InvalidStockChange", ex.Message));
            }

            _unitOfWork.WriteRepository<Product>().Update(product);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
