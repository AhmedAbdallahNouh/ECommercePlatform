using ECommerce.Application.Abstraction.Messaging;
using ECommerce.Application.Payments.DTOs;

namespace ECommerce.Application.Payments.Queries.GetAllPayments
{
    public sealed record GetAllPaymentsQuery() : IQuery<IReadOnlyList<PaymentDto>>;
}
