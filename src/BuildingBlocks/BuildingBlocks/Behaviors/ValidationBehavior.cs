using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;

namespace BuildingBlocks.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse>
        (IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var ctx = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(validators.Select(x => x.ValidateAsync(ctx, cancellationToken)));

            var failtures = validationResults.Where(x => x.Errors.Any())
                .SelectMany(x => x.Errors)
                .ToList();

            if (failtures.Any())
            {
                throw new ValidationException(failtures);
            }

            return await next();

        }
    }
}
