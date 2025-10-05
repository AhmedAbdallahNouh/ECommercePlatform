using ECommerce.Application.Abstraction.Messaging;
using ECommerce.Application.Categories.DTOs.ECommerce.Application.Categories.DTOs;

namespace ECommerce.Application.Categories.Queries.GetCategoryById
{
    public sealed record GetCategoryByIdQuery(int Id) : IQuery<CategoryDto>;
}
