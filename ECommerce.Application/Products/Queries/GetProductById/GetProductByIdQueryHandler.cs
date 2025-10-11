using ECommerce.Application.Abstraction.Messaging;
using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Products.DTOs;
using ECommerce.Application.Products.Queries.Specifications;
using ECommerce.Domain.Models;
using ECommerce.Domain.Shared;

namespace ECommerce.Application.Products.Queries.GetProductById
{
    public class GetProductByIdQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetProductByIdQeury, ProductDto>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result<ProductDto>> Handle(GetProductByIdQeury request, CancellationToken cancellationToken)
        {
            var spec = new ProductByIdSpec(request.id);
            var product = await _unitOfWork.ReadRepository<Product>().GetEntityWithSpecAsync(spec);

            if (product is not null)
            {
                var productDto = new ProductDto(
                       product.Id,
                       product.Name,
                       product.Price,
                       product.Stock,
                       product.Category?.Name ?? string.Empty,
                       product.Description ?? string.Empty
                    );

                return Result.Success(productDto);
            }
            return Result.Failure<ProductDto>(Error.NullValue);         
        }
    }
}
