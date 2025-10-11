using ECommerce.Application.Abstraction.Messaging;
using ECommerce.Application.Common.Interfaces;
using ECommerce.Domain.Models;
using ECommerce.Domain.Shared;

namespace ECommerce.Application.Products.Commands.CreateProductCommand
{
    internal class CreateProductCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<CreateProductCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<Result<int>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.ReadRepository<Category>().GetByIdAsync(request.CategoryId);
            if (category is null)
                return Result.Failure<int>(new Error("Category.NotFound", "Category does not exist."));


            var existingProduct = (await _unitOfWork.ReadRepository<Product>().GetAllAsync())
               .FirstOrDefault(p => p.Name.ToLower() == request.Name.ToLower() && p.CategoryId == request.CategoryId);

            if (existingProduct is not null)
                return Result.Failure<int>(new Error("Product.Duplicate", "Product with the same name already exists in this category."));

            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Stock = request.Stock,
                CategoryId = request.CategoryId,
                IsDeleted = false
            };
            await _unitOfWork.WriteRepository<Product>().AddAsync(product);

            var result = await _unitOfWork.SaveChangesAsync();

            return result > 0 ? Result.Success(product.Id) : Result.Failure<int>(Error.NullValue);
        }
    }
}
