using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace PP.Core.Features.Shared.Commands;

public abstract class PersonaCommandHandler<TRequest, TResponse, TValidator>(IServiceScopeFactory scopeFactory)
    : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TValidator : AbstractValidator<TRequest>
        where TResponse : PersonaCommandResponse, new()
{
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        using var scope = scopeFactory.CreateAsyncScope();

        // Validate
        var validationResult = await Validate(request, scope);

        // Exit prematurely if validation fails
        if (!validationResult.IsValid)
        {
            return new TResponse()
            {
                Errors = validationResult.Errors.ToDictionary(k => k.PropertyName, v => v.ErrorMessage)
            };
        }

        // Exit the actual command
        return await Execute(request, cancellationToken);
    }

    protected abstract Task<TResponse> Execute(TRequest request, CancellationToken cancellationToken);

    private async Task<ValidationResult> Validate(TRequest request, AsyncServiceScope scope)
    {
        // Create the validator
        var validator = scope.ServiceProvider.GetService<TValidator>();

        if (validator is null)
        {
            throw new Exception($"Failed to find service for {typeof(TValidator).Name}");
        }
        
        // Perform validation
        return await validator.ValidateAsync(request);
    }

}