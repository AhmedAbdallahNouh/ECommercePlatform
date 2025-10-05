using ECommerce.Application.Abstraction.Messaging;
using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Products.DTOs;
using ECommerce.Domain.Models;
using ECommerce.Domain.Shared;

namespace ECommerce.Application.Products.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler(IUnitOfWork unitOfWork)
        : IQeuryHandler<GetAllProductsQuery, IReadOnlyList<ProductDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result<IReadOnlyList<ProductDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.ReadRepository<Product>().GetAllAsync();

            if(products.Any())
            {
                var list = products.Select(product => new ProductDto(
                    product.Id,
                    product.Name,
                    product.Price,
                    product.Stock,
                    product.Description ?? string.Empty
                )).ToList();

                return Result.Success((IReadOnlyList<ProductDto>)list);
            }

            return Result.Failure<IReadOnlyList<ProductDto>>(Error.NullValue);


        }
    }
}
