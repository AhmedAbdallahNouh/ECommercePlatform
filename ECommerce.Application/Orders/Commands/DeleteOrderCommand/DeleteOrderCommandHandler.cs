using ECommerce.Application.Abstraction.Messaging;
using ECommerce.Application.Common.Interfaces;
using ECommerce.Domain.Models;
using ECommerce.Domain.Shared;

namespace ECommerce.Application.Orders.Commands.DeleteOrder
{
    internal class DeleteOrderCommandHandler(IUnitOfWork unitOfWork)
        : ICommandHandler<DeleteOrderCommand>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.ReadRepository<Order>().GetByIdAsync(request.Id);
            if (order is null)
                return Result.Failure(Error.NullValue);

            _unitOfWork.WriteRepository<Order>().Delete(order);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
