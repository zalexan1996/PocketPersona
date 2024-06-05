using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PP.Core.Features.Shared.Commands;
using PP.Core.Features.Shared.Services;
using PP.Core.Services;

namespace PP.Core.Features.Character.Commands;

public record AddCharacterCommand(string Name, int GameId, IList<string> Gifts) : IRequest<PersonaCommandResponse>;

public class AddCharacterCommandHandler(IServiceScopeFactory scopeFactory, IDbService dbService)
    : PersonaCommandHandler<AddCharacterCommand, PersonaCommandResponse, AddCharacterCommandValidator>(scopeFactory)
{
    protected override async Task<PersonaCommandResponse> Execute(AddCharacterCommand request, CancellationToken cancellationToken)
    {
        dbService.Add(new Domain.Entities.Character()
        {
            GameId = request.GameId,
            Gifts = request.Gifts,
            Name = request.Name
        });

        await dbService.SaveChanges(cancellationToken);

        return new PersonaCommandResponse();
    }
}

public class AddCharacterCommandValidator : AbstractValidator<AddCharacterCommand>
{
    public AddCharacterCommandValidator(PersonaValidationLibrary library)
    {
        RuleFor(x => x.Name)
            .NotEmpty()
                .WithMessage("You must specify a valid name.")
            .MustAsync(library.CharacterNameNotInUse)
                .WithMessage("That character already exists.");

        RuleFor(x => x.GameId)
            .MustAsync(library.GameMustExist)
                .WithMessage("You must provide a valid GameId.");
    }
}