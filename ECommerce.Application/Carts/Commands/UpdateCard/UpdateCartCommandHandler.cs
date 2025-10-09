using ECommerce.Application.Abstraction.Messaging;
using ECommerce.Application.Common.Interfaces;
using ECommerce.Domain.Models;
using ECommerce.Domain.Shared;

namespace ECommerce.Application.Carts.Commands.UpdateCart
{
    internal sealed class UpdateCartCommandHandler : ICommandHandler<UpdateCartCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCartCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _unitOfWork.ReadRepository<Cart>().GetByIdAsync(request.Id);
            if (cart is null)
                return Result.Failure(Error.NullValue);

            cart.Items.Clear();
            foreach (var i in request.Items)
            {
                cart.Items.Add(new CartItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    //Price = i.Price
                });
            }

            cart.TotalAmount = request.Items.Sum(i => i.Price * i.Quantity);
            _unitOfWork.WriteRepository<Cart>().Update(cart);
            var saved = await _unitOfWork.SaveChangesAsync();

            return saved > 0 ? Result.Success() : Result.Failure(Error.Failure);
        }
    }
}
