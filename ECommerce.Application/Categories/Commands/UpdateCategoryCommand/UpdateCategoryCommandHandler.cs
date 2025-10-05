using ECommerce.Application.Abstraction.Messaging;
using ECommerce.Application.Common.Interfaces;
using ECommerce.Domain.Models;
using ECommerce.Domain.Shared;

namespace ECommerce.Application.Categories.Commands.UpdateCategory
{
    internal class UpdateCategoryCommandHandler(IUnitOfWork unitOfWork)
        : ICommandHandler<UpdateCategoryCommand>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.ReadRepository<Category>().GetByIdAsync(request.Id);
            if (category is null)
                return Result.Failure(Error.NullValue);

            category.Name = request.Name;
            //category.Description = request.Description;

            _unitOfWork.WriteRepository<Category>().Update(category);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
