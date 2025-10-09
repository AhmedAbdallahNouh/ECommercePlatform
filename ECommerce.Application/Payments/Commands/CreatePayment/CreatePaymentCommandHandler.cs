using ECommerce.Application.Abstraction.Messaging;
using ECommerce.Application.Common.Interfaces;
using ECommerce.Domain.Models;
using ECommerce.Domain.Shared;

namespace ECommerce.Application.Payments.Commands.CreatePayment
{
    internal sealed class CreatePaymentCommandHandler(IUnitOfWork unitOfWork)
        : ICommandHandler<CreatePaymentCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result<int>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = new Payment
            {
                OrderId = request.OrderId,
                Amount = request.Amount,
                Method = request.Method,
                Status = request.Status,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.WriteRepository<Payment>().AddAsync(payment);
            var result = await _unitOfWork.SaveChangesAsync();

            return result > 0
                ? Result.Success(payment.Id)
                : Result.Failure<int>(Error.NullValue);
        }
    }
}
