using ECommerce.Application.Abstraction.Messaging;
using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Products.DTOs;
using ECommerce.Domain.Models;
using ECommerce.Domain.Shared;

namespace ECommerce.Application.Products.Qeuries.GetProductById
{
    public class GetProductByIdQeuryHandler(IUnitOfWork unitOfWork) : IQeuryHandler<GetProductByIdQeury, ProductDto>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result<ProductDto>> Handle(GetProductByIdQeury request, CancellationToken cancellationToken)
        {
            await _unitOfWork.Repository<Product>().GetByIdAsync(request.id);
            
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(request.id);
            if (product is not null)
            {
                var productDto = new ProductDto(
                       product.Id,
                       product.Name,
                       product.Price,
                       product.Stock,
                       product.Description ?? string.Empty
                    );

                return Result.Success(productDto);
            }
            return Result.Failure<ProductDto>(Error.NullValue);
            
        }
    }
}
