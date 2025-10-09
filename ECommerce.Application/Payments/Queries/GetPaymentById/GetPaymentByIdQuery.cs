using ECommerce.Application.Abstraction.Messaging;
using ECommerce.Application.Payments.DTOs;

namespace ECommerce.Application.Payments.Queries.GetPaymentById
{
    public sealed record GetPaymentByIdQuery(int Id) : IQuery<PaymentDto>;
}
