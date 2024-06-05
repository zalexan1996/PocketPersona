using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PP.Core.Features.Shared.Commands;
using PP.Core.Features.Shared.Services;
using PP.Core.Services;

namespace PP.Core.Features.Commands;

public record AddGameCommand(string Name) : IRequest<PersonaCommandResponse>;

public class AddGameCommandHandler(IDbService dbService, IServiceScopeFactory scopeFactory)
    : PersonaCommandHandler<AddGameCommand, PersonaCommandResponse, AddGameCommandValidator>(scopeFactory)
{
    protected override async Task<PersonaCommandResponse> Execute(AddGameCommand request, CancellationToken cancellationToken)
    {
        dbService.Add(new Domain.Entities.Game()
        {
            Name = request.Name
        });

        await dbService.SaveChanges(cancellationToken);

        return new PersonaCommandResponse();
    }
}

public class AddGameCommandValidator : AbstractValidator<AddGameCommand>
{
    public AddGameCommandValidator(PersonaValidationLibrary library)
    {
        RuleFor(x => x.Name)
            .MustAsync(library.GameNameNotInUse)
                .WithMessage("That game already exists.");
    }
}