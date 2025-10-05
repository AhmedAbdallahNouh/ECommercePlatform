using ECommerce.Application.Abstraction.Messaging;
using ECommerce.Application.Categories.DTOs.ECommerce.Application.Categories.DTOs;
using ECommerce.Application.Common.Interfaces;
using ECommerce.Domain.Models;
using ECommerce.Domain.Shared;

namespace ECommerce.Application.Categories.Queries.GetAllCategories
{
    internal class GetAllCategoriesQueryHandler(IUnitOfWork unitOfWork)
        : IQueryHandler<GetAllCategoriesQuery, IReadOnlyList<CategoryDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result<IReadOnlyList<CategoryDto>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _unitOfWork.ReadRepository<Category>().GetAllAsync();

            var list = categories.Select(c => new CategoryDto(c.Id, c.Name ?? string.Empty)).ToList();

            return Result.Success((IReadOnlyList<CategoryDto>)list);
        }
    }
}
