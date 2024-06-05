using System.Data.Common;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PP.Core.Features.Shared.Commands;
using PP.Core.Features.Shared.Services;
using PP.Core.Services;

namespace PP.Core.Features.Character.Commands;

public record UpdateCharacterCommand(int Id, int GameId, string Name, IList<string> Gifts) : IRequest<PersonaCommandResponse>;

public class UpdateCharacterCommandHandler(IServiceScopeFactory scopeFactory, IDbService dbService)
    : PersonaCommandHandler<UpdateCharacterCommand, PersonaCommandResponse, UpdateCharacterValidator>(scopeFactory)
{
    protected override async Task<PersonaCommandResponse> Execute(UpdateCharacterCommand request, CancellationToken cancellationToken)
    {
        var record = await dbService.Set<Domain.Entities.Character>().SingleAsync(c => c.Id == request.Id, cancellationToken);

        record.Name = request.Name;
        record.Gifts = request.Gifts;
        record.GameId = request.GameId;

        await dbService.SaveChanges(cancellationToken);

        return new PersonaCommandResponse();
    }
}

public class UpdateCharacterValidator : AbstractValidator<UpdateCharacterCommand>
{
    private readonly IDbService _dbService;
    private readonly PersonaValidationLibrary _library;
    
    public UpdateCharacterValidator(IDbService dbService, PersonaValidationLibrary library)
    {
        _dbService = dbService;
        _library = library;

        RuleFor(x => x.Id)
            .MustAsync(_library.CharacterMustExist)
                .WithMessage("The specified Id did not belong to a valid character.");

        RuleFor(x => x.Name)
            .NotEmpty()
                .WithMessage("You must provide a valid name.")
            .MustAsync(NotBelongToAnotherCharacter)
                .WithMessage("That name belongs to an existing character.");

        RuleFor(x => x.GameId)
            .MustAsync(_library.GameMustExist)
                .WithMessage("You must provide a valid GameId.");
    }

    private async Task<bool> NotBelongToAnotherCharacter(UpdateCharacterCommand command, string name, CancellationToken cancellationToken)
    {
        var character = await _dbService.Set<Domain.Entities.Character>().SingleOrDefaultAsync(c => c.Name == name, cancellationToken);

        if (character is null)
        {
            return true;
        }

        return character.Id == command.Id;
    }
}