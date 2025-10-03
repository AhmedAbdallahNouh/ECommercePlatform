using ECommerce.Domain.Shared;
using MediatR;


namespace ECommerce.Application.Abstraction.Messaging
{
    public interface IQeuryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
        where TQuery : IQuery<TResponse>
    {
    }
}
