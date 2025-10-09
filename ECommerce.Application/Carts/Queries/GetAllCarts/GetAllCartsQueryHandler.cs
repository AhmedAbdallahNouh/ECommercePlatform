using ECommerce.Application.Abstraction.Messaging;
using ECommerce.Application.Carts.DTOs;
using ECommerce.Application.Common.Interfaces;
using ECommerce.Domain.Models;
using ECommerce.Domain.Shared;

namespace ECommerce.Application.Carts.Queries.GetAllCarts
{
    internal sealed class GetAllCartsQueryHandler : IQueryHandler<GetAllCartsQuery, IReadOnlyList<CartDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllCartsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IReadOnlyList<CartDto>>> Handle(GetAllCartsQuery request, CancellationToken cancellationToken)
        {
            var carts = await _unitOfWork.ReadRepository<Cart>().GetAllAsync();

            var list = carts.Select(c => new CartDto(
                c.Id,
                c.UserId,
                c.TotalAmount,
                c.Items.Select(i => new CartItemDto(i.ProductId, i.Quantity)).ToList()
            )).ToList();

            return Result.Success((IReadOnlyList<CartDto>)list);
        }
    }
}
