using ECommerce.Application.Abstraction.Messaging;
using ECommerce.Application.Common.Interfaces;
using ECommerce.Domain.Models;
using ECommerce.Domain.Shared;

namespace ECommerce.Application.Carts.Commands.CreateCart
{
    internal sealed class CreateCartCommandHandler : ICommandHandler<CreateCartCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCartCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            if (request.Items == null || !request.Items.Any())
                return Result.Failure<int>(new Error("Error.Validation", "Cart must contain at least one item."));

            var total = request.Items.Sum(i => i.Price * i.Quantity);

            var cart = new Cart
            {
                UserId = request.UserId,
                Items = request.Items.Select(i => new CartItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                }).ToList(),
                TotalAmount = total
            };

            await _unitOfWork.WriteRepository<Cart>().AddAsync(cart);
            var saved = await _unitOfWork.SaveChangesAsync();

            return saved > 0 ? Result.Success(cart.Id) : Result.Failure<int>(Error.Failure);
        }
    }
}
