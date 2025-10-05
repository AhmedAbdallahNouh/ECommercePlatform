using ECommerce.Application.Abstraction.Messaging;
using ECommerce.Application.Categories.DTOs;
using ECommerce.Application.Categories.DTOs.ECommerce.Application.Categories.DTOs;

namespace ECommerce.Application.Categories.Queries.GetAllCategories
{
    public sealed record GetAllCategoriesQuery() : IQuery<IReadOnlyList<CategoryDto>>;
}
