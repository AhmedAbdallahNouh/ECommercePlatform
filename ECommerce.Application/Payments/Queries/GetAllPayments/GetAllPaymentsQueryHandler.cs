using ECommerce.Application.Abstraction.Messaging;
using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Payments.DTOs;
using ECommerce.Domain.Models;
using ECommerce.Domain.Shared;

namespace ECommerce.Application.Payments.Queries.GetAllPayments
{
    internal sealed class GetAllPaymentsQueryHandler : IQueryHandler<GetAllPaymentsQuery, IReadOnlyList<PaymentDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllPaymentsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IReadOnlyList<PaymentDto>>> Handle(GetAllPaymentsQuery request, CancellationToken cancellationToken)
        {
            var payments = await _unitOfWork.ReadRepository<Payment>().GetAllAsync();

            var list = payments.Select(p => new PaymentDto(
                p.Id,
                p.OrderId,
                p.Amount,
                p.Method,
                p.Status,
                p.CreatedAt,
                p.UpdatedAt
            )).ToList();

            return Result.Success((IReadOnlyList<PaymentDto>)list);
        }
    }
}
