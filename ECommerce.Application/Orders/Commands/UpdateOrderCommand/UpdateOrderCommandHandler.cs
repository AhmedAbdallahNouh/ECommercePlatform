using ECommerce.Application.Abstraction.Messaging;
using ECommerce.Application.Common.Interfaces;
using ECommerce.Domain.Models;
using ECommerce.Domain.Shared;

namespace ECommerce.Application.Orders.Commands.UpdateOrder
{
    internal class UpdateOrderCommandHandler(IUnitOfWork unitOfWork)
        : ICommandHandler<UpdateOrderCommand>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.ReadRepository<Order>().GetByIdAsync(request.Id);
            if (order is null)
                return Result.Failure(Error.NullValue);

            order.Status = request.Status;
            _unitOfWork.WriteRepository<Order>().Update(order);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
