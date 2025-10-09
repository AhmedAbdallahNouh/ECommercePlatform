using ECommerce.Application.Abstraction.Messaging;

namespace ECommerce.Application.Payments.Commands.CreatePayment
{
    public sealed record CreatePaymentCommand(
        int OrderId,
        decimal Amount,
        string Method,
        string Status
    ) : ICommand<int>;
}
