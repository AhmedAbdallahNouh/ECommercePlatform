using ECommerce.Application.Abstraction.Messaging;

namespace ECommerce.Application.Payments.Commands.UpdatePayment
{
    public sealed record UpdatePaymentCommand(
        int Id,
        decimal Amount,
        string Method,
        string Status
    ) : ICommand;
}
