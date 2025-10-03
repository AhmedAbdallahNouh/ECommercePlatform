using ECommerce.Application.Abstraction.Messaging;
using ECommerce.Application.Products.DTOs;

namespace ECommerce.Application.Products.Qeuries.GetProductById
{
    public sealed record GetProductByIdQeury(int id) : IQuery<ProductDto>
    {
        
    }
}
