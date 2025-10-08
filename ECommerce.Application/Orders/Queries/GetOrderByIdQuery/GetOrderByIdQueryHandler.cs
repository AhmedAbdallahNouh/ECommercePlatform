using ECommerce.Application.Abstraction.Messaging;
using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Orders.DTOs;
using ECommerce.Application.Orders.Specifications;
using ECommerce.Domain.Models;
using ECommerce.Domain.Shared;

namespace ECommerce.Application.Orders.Queries.GetOrderById
{
    internal class GetOrderByIdQueryHandler(IUnitOfWork unitOfWork)
        : IQueryHandler<GetOrderByIdQuery, OrderDto>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result<OrderDto>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {

            var spec = new OrderByIdWithItemsSpecification(request.Id);

            var order = await _unitOfWork.ReadRepository<Order>().GetEntityWithSpecAsync(spec);

            if (order is null)
                return Result.Failure<OrderDto>(Error.NullValue);

            var dto = new OrderDto(
                order.Id,
                //order.UserId,
                "dsdadas",
                order.TotalAmount,
                order.Status,
                order.Date,
                order.OrderItems.Select(i => new OrderItemDto(i.ProductId, i.Quantity, i.Price)).ToList()
            );

            return Result.Success(dto);
        }
    }
}
