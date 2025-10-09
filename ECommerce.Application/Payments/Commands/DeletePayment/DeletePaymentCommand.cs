using ECommerce.Application.Abstraction.Messaging;

namespace ECommerce.Application.Payments.Commands.DeletePayment
{
    public sealed record DeletePaymentCommand(int Id) : ICommand;
}
