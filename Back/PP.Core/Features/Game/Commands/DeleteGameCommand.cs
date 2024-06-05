using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PP.Core.Features.Shared.Commands;
using PP.Core.Features.Shared.Services;
using PP.Core.Services;

namespace PP.Core.Features.Game.Commands;

public record DeleteGameCommand(int Id) : IRequest<PersonaCommandResponse>;

public class DeleteGameCommandHandler(IServiceScopeFactory scopeFactory, IDbService dbService) : PersonaCommandHandler<DeleteGameCommand, PersonaCommandResponse, DeleteGameCommandValidator>(scopeFactory)
{
    protected override async Task<PersonaCommandResponse> Execute(DeleteGameCommand request, CancellationToken cancellationToken)
    {
        var game = await dbService.Set<Domain.Entities.Game>().SingleAsync(g => g.Id == request.Id, cancellationToken);
        
        dbService.Remove(game);
        await dbService.SaveChanges(cancellationToken);

        return new PersonaCommandResponse();
    }
}

public class DeleteGameCommandValidator : AbstractValidator<DeleteGameCommand>
{
    public DeleteGameCommandValidator(PersonaValidationLibrary library)
    {
        RuleFor(g => g.Id)
            .MustAsync(library.GameMustExist)
                .WithMessage("The specified Id did not belong to a valid game.");
    }
}