using ECommerce.Application.Abstraction.Messaging;
using ECommerce.Application.Common.Interfaces;
using ECommerce.Domain.Models;
using ECommerce.Domain.Shared;

namespace ECommerce.Application.Categories.Commands.DeleteCategory
{
    internal class DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
        : ICommandHandler<DeleteCategoryCommand>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.ReadRepository<Category>().GetByIdAsync(request.Id);
            if (category is null)
                return Result.Failure(Error.NullValue);

            _unitOfWork.WriteRepository<Category>().Delete(category);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
