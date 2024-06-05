using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PP.Core.Features.Shared.Commands;
using PP.Core.Features.Shared.Services;
using PP.Core.Services;

namespace PP.Core.Features.Game.Commands;

public record UpdateGameCommand(int Id, string Name) : IRequest<PersonaCommandResponse>;

public class UpdateGameCommandHandler(IServiceScopeFactory scopeFactory, IDbService dbService)
    : PersonaCommandHandler<UpdateGameCommand, PersonaCommandResponse, UpdateGameCommandValidator>(scopeFactory)
{
    protected override async Task<PersonaCommandResponse> Execute(UpdateGameCommand request, CancellationToken cancellationToken)
    {
        var game = await dbService.Set<Domain.Entities.Game>().SingleAsync(g => g.Id == request.Id);
        game.Name = request.Name;
        await dbService.SaveChanges(cancellationToken);

        return new PersonaCommandResponse();
    }
}

public class UpdateGameCommandValidator : AbstractValidator<UpdateGameCommand>
{
    private readonly IDbService _dbService;

    public UpdateGameCommandValidator(IDbService dbService, PersonaValidationLibrary library)
    {
        _dbService = dbService;

        RuleFor(c => c.Id)
            .MustAsync(library.GameMustExist)
            .WithMessage("The specified Id did not belong to a valid game.");

        RuleFor(c => c.Name)
            .MustAsync(NotBelongToAnotherGame)
            .WithMessage("The specified name already belongs to a valid game.");
    }

    private async Task<bool> NotBelongToAnotherGame(UpdateGameCommand command, string name, CancellationToken cancellationToken)
    {
        var game = await _dbService.Set<Domain.Entities.Game>().SingleOrDefaultAsync(g => g.Name == name);
        if (game is null)
        {
            return true;
        }

        return game.Id == command.Id;
    }
}