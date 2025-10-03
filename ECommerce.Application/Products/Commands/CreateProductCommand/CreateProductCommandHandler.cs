using ECommerce.Application.Abstraction.Messaging;
using ECommerce.Application.Common.Interfaces;
using ECommerce.Domain.Models;
using ECommerce.Domain.Shared;
using MediatR;

namespace ECommerce.Application.Products.Commands.CreateProductCommand
{
    internal class CreateProductCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<CreateProductCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<Result<int>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {

            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Stock = request.Stock,
                CategoryId = request.CategoryId
            };
            await _unitOfWork.Repository<Product>().AddAsync(product);

            var result = await _unitOfWork.SaveChangesAsync();

            return result > 0 ? Result.Success(product.Id) : Result.Failure<int>(Error.NullValue);
        }
    }
}
