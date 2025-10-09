using ECommerce.Application.Abstraction.Messaging;
using ECommerce.Application.Common.Interfaces;
using ECommerce.Domain.Models;
using ECommerce.Domain.Shared;

namespace ECommerce.Application.Payments.Commands.DeletePayment
{
    internal sealed class DeletePaymentCommandHandler : ICommandHandler<DeletePaymentCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeletePaymentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await _unitOfWork.ReadRepository<Payment>().GetByIdAsync(request.Id);
            if (payment is null)
                return Result.Failure(Error.NullValue);

            _unitOfWork.WriteRepository<Payment>().Delete(payment);
            var saved = await _unitOfWork.SaveChangesAsync();

            return saved > 0 ? Result.Success() : Result.Failure(Error.Failure);
        }
    }
}
