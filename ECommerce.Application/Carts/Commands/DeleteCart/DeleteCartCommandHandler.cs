using ECommerce.Application.Abstraction.Messaging;
using ECommerce.Application.Common.Interfaces;
using ECommerce.Domain.Models;
using ECommerce.Domain.Shared;

namespace ECommerce.Application.Carts.Commands.DeleteCart
{
    internal sealed class DeleteCartCommandHandler : ICommandHandler<DeleteCartCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCartCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _unitOfWork.ReadRepository<Cart>().GetByIdAsync(request.Id);
            if (cart is null)
                return Result.Failure(Error.NullValue);

            _unitOfWork.WriteRepository<Cart>().Delete(cart);
            var saved = await _unitOfWork.SaveChangesAsync();
            return saved > 0 ? Result.Success() : Result.Failure(Error.Failure);
        }
    }
}
