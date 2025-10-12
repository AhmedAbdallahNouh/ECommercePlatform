using ECommerce.Application.Abstraction.Messaging;
using ECommerce.Application.Common.Interfaces;
using ECommerce.Domain.Models;
using ECommerce.Domain.Shared;
using MediatR;

namespace ECommerce.Application.Products.Commands.UpdateProductCommand
{
    public class UpdateProductCommandHandler(IUnitOfWork unitOfWork, ISearchService meiliSearchService)
        : ICommandHandler<UpdateProductCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ISearchService _meiliSearchService = meiliSearchService;


        async Task<Result<int>> IRequestHandler<UpdateProductCommand, Result<int>>.Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.ReadRepository<Product>().GetByIdAsync(request.Id);
            if (product is null)
                return Result.Failure<int>(Error.NullValue);

            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.Stock = request.Stock;
            product.Stock = request.Stock;
            //product.CategoryId = request.CategoryId;

            _unitOfWork.WriteRepository<Product>().Update(product);
            var result = await _unitOfWork.SaveChangesAsync();

            await _meiliSearchService.AddOrUpdateProductAsync(product);

            return result > 0 ? Result.Success(result) : Result.Failure<int>(Error.NullValue);
        }
    }
}
