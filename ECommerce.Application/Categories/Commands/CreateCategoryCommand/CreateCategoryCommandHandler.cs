using ECommerce.Application.Abstraction.Messaging;
using ECommerce.Application.Common.Interfaces;
using ECommerce.Domain.Models;
using ECommerce.Domain.Shared;

namespace ECommerce.Application.Categories.Commands.CreateCategory
{
    internal class CreateCategoryCommandHandler(IUnitOfWork unitOfWork)
        : ICommandHandler<CreateCategoryCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result<int>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Name = request.Name,
            };

            await _unitOfWork.WriteRepository<Category>().AddAsync(category);
            var result = await _unitOfWork.SaveChangesAsync();

            return result > 0
                ? Result.Success(category.Id)
                : Result.Failure<int>(Error.NullValue);
        }
    }
}
