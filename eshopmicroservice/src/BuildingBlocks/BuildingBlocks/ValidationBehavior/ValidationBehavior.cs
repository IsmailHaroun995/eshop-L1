using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;

namespace BuildingBlocks.ValidationBehavior;

public class ValidationBehavior<TRequest, TResponse> 
    (IEnumerable<IValidator<TRequest>>validators)
    : IPipelineBehavior<TRequest, TResponse> 
    where TRequest :ICommand<TRequest>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate
        <TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        var validationresult = await Task.WhenAll(validators.
            Select(x => x.ValidateAsync(context,cancellationToken)));

        var failuers = validationresult.Where(r => r.Errors.Any())
            .SelectMany(r => r.Errors).ToList();

        return await next();
    }
}

