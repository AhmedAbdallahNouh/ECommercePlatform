using ECommerce.Application.Abstraction.Messaging;
using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Payments.DTOs;
using ECommerce.Domain.Models;
using ECommerce.Domain.Shared;

namespace ECommerce.Application.Payments.Queries.GetPaymentById
{
    internal sealed class GetPaymentByIdQueryHandler(IUnitOfWork unitOfWork)
        : IQueryHandler<GetPaymentByIdQuery, PaymentDto>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result<PaymentDto>> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
        {
            var payment = await _unitOfWork.ReadRepository<Payment>().GetByIdAsync(request.Id);
            if (payment is null)
                return Result.Failure<PaymentDto>(Error.NullValue);

            var dto = new PaymentDto(
                payment.Id,
                payment.OrderId,
                payment.Amount,
                payment.Method,
                payment.Status,
                payment.CreatedAt,
                payment.UpdatedAt
            );

            return Result.Success(dto);
        }
    }
}
