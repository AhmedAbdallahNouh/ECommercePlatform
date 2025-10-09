using ECommerce.Application.Abstraction.Messaging;
using ECommerce.Application.Common.Interfaces;
using ECommerce.Domain.Models;
using ECommerce.Domain.Shared;

namespace ECommerce.Application.Payments.Commands.UpdatePayment
{
    internal sealed class UpdatePaymentCommandHandler(IUnitOfWork unitOfWork)
        : ICommandHandler<UpdatePaymentCommand>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await _unitOfWork.ReadRepository<Payment>().GetByIdAsync(request.Id);
            if (payment is null)
                return Result.Failure(Error.NullValue);

            payment.Amount = request.Amount;
            payment.Method = request.Method;
            payment.Status = request.Status;
            payment.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.WriteRepository<Payment>().Update(payment);
            var result = await _unitOfWork.SaveChangesAsync();

            return result > 0 ? Result.Success() : Result.Failure(Error.NullValue);
        }
    }
}
