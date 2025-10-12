using ECommerce.Application.Abstraction.Messaging;
using ECommerce.Application.Carts.DTOs;
using ECommerce.Application.Common.Interfaces;
using ECommerce.Domain.Models;
using ECommerce.Domain.Shared;

namespace ECommerce.Application.Carts.Queries.GetCartById
{
    internal sealed class GetCartByIdQueryHandler : IQueryHandler<GetCartByIdQuery, CartDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCartByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<CartDto>> Handle(GetCartByIdQuery request, CancellationToken cancellationToken)
        {
            var cart = await _unitOfWork.ReadRepository<Cart>().GetByIdAsync(request.Id);
            if (cart is null)
                return Result.Failure<CartDto>(Error.NullValue);

            var dto = new CartDto(
                cart.Id,
                cart.UserId,
                cart.TotalAmount,
                cart.Items.Select(i => new CartItemDto(i.ProductId, i.Quantity, i.Price)).ToList()
            );

            return Result.Success(dto);
        }
    }
}
