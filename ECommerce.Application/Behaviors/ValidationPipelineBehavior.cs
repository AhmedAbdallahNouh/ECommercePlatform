using ECommerce.Domain.Shared;
using FluentValidation;
using MediatR;

namespace ECommerce.Application.Behaviors
{
    public class ValidationPipelineBehavior<TRequest, TResponse> :
       IPipelineBehavior<TRequest, TResponse>
       where TRequest : IRequest<TResponse>
       where TResponse : Result
    {
        public readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            //Validate request 
            //if not valid return failure result
            //otherwise call the next delegate  next();

            if (!_validators.Any())
                return await next();

            Error[] errors = _validators
                .Select(validator => validator.Validate(request))
                .SelectMany(validationResult => validationResult.Errors)
                .Where(validatorError => validatorError is not null)
                .Select(failur => new Error(
                    failur.PropertyName,
                    failur.ErrorMessage
                    ))
                .Distinct()
                .ToArray();

            if (errors.Any())
            {

                return CreateValidationResult<TResponse>(errors);
                //Return Validation result
            }
            throw new NotImplementedException();
        }

        private static TResult CreateValidationResult<TResult>(Error[] errors)
            where TResult : Result
        {
            if(typeof(TResult) == typeof(Result))
            {
                return (ValidationResult.WithErrors(errors) as TResult)!;
            }

           object validationReult =  typeof(ValidationResultT<>)
                .GetGenericTypeDefinition()
                .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
                .GetMethod(nameof(ValidationResult.WithErrors))!
                .Invoke(null, new object?[] { errors })!;


            return (TResult)validationReult;
        }
    }
}
