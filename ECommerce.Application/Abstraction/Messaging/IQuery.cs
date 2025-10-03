using ECommerce.Domain.Shared;
using MediatR;

namespace ECommerce.Application.Abstraction.Messaging
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {
    }
    
}
