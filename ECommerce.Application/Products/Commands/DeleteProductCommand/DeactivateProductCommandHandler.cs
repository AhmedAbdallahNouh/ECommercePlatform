using ECommerce.Application.Abstraction.Messaging;
using ECommerce.Application.Common.Interfaces;
using ECommerce.Domain.Models;
using ECommerce.Domain.Shared;

namespace ECommerce.Application.Products.Commands.DeleteProductCommand
{
    public class DeactivateProductCommandHandler(IUnitOfWork unitOfWork , ISearchService meiliSearchService)
        : ICommandHandler<DeactivateProductCommand>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ISearchService _meiliSearchService = meiliSearchService;

        public async Task<Result> Handle(DeactivateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.ReadRepository<Product>().GetByIdAsync(request.Id);
            if (product is null)
                return Result.Failure(Error.NullValue);

            _unitOfWork.WriteRepository<Product>().Delete(product);
            var result = await _unitOfWork.SaveChangesAsync();

            await _meiliSearchService.DeleteProductAsync(product.Id);

            return result > 0 ? Result.Success() : Result.Failure(Error.NullValue);
        }
    }
}
