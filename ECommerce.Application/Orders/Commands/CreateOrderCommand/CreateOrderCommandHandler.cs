using ECommerce.Application.Abstraction.Messaging;
using ECommerce.Application.Common.Interfaces;
using ECommerce.Domain.Models;
using ECommerce.Domain.Shared;

namespace ECommerce.Application.Orders.Commands.CreateOrder
{
    internal class CreateOrderCommandHandler(IUnitOfWork unitOfWork)
        : ICommandHandler<CreateOrderCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result<int>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                UserId = request.UserId,
                TotalAmount = request.TotalAmount,
                Status = request.Status,
                Date = DateTime.UtcNow,
                OrderItems = request.Items.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    Price = i.Price
                }).ToList()
            };

            await _unitOfWork.WriteRepository<Order>().AddAsync(order);
            var result = await _unitOfWork.SaveChangesAsync();

            return result > 0
                ? Result.Success(order.Id)
                : Result.Failure<int>(Error.NullValue);
        }
    }
}
