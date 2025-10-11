using ECommerce.Application.Abstraction.Messaging;
using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Products.DTOs;
using ECommerce.Application.Products.Queries.Specifications;
using ECommerce.Domain.Models;
using ECommerce.Domain.Shared;

namespace ECommerce.Application.Products.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler(IUnitOfWork unitOfWork)
        : IQueryHandler<GetAllProductsQuery, IReadOnlyList<ProductDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result<IReadOnlyList<ProductDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var skip = (request.PageNumber - 1) * request.PageSize;
            var spec = ProductSpecifications.AllActiveProducts(request.SearchTerm, request.CategoryId , skip, request.PageSize);

            var products = await _unitOfWork.ReadRepository<Product>().GetEntitiesWithSpecAsync(spec);

            if(products.Any())
            {
                var list = products.Select(product => new ProductDto(
                    product.Id,
                    product.Name,
                    product.Price,
                    product.Stock,
                    product.Category?.Name ?? string.Empty,
                    product.Description ?? string.Empty

                )).ToList();

                return Result.Success((IReadOnlyList<ProductDto>)list);
            }

            return Result.Failure<IReadOnlyList<ProductDto>>(Error.NullValue);


        }
    }
}
