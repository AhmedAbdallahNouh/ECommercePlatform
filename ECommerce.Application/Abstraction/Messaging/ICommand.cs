
using ECommerce.Domain.Shared;
using MediatR;

namespace ECommerce.Application.Abstraction.Messaging
{
    public  interface ICommand : IRequest<Result> 
    {
    }

    public  interface ICommand<TResponse> : IRequest<Result<TResponse>> 
    {
    }
}
