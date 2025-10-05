using ECommerce.Application.Abstraction.Messaging;
using ECommerce.Application.Categories.DTOs.ECommerce.Application.Categories.DTOs;
using ECommerce.Application.Common.Interfaces;
using ECommerce.Domain.Models;
using ECommerce.Domain.Shared;

namespace ECommerce.Application.Categories.Queries.GetCategoryById
{
    internal class GetCategoryByIdQueryHandler(IUnitOfWork unitOfWork)
        : IQueryHandler<GetCategoryByIdQuery, CategoryDto>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.ReadRepository<Category>().GetByIdAsync(request.Id);
            if (category is null)
                return Result.Failure<CategoryDto>(Error.NullValue);

            var dto = new CategoryDto(category.Id, category.Name ?? string.Empty);
            return Result.Success(dto);
        }
    }
}
