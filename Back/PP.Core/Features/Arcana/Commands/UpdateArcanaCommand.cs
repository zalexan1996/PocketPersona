using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PP.Core.Features.Shared.Commands;
using PP.Core.Services;
using PP.Domain;

namespace PP.Core.Features.Arcana;

public record UpdateArcanaCommand(int Id, string Name, IList<int> GameIds) : IRequest<PersonaCommandResponse>;

public class UpdateArcanaCommandHandler(IServiceScopeFactory scopeFactory, IDbService dbService)
    : PersonaCommandHandler<UpdateArcanaCommand, PersonaCommandResponse, UpdateArcanaCommandValidator>(scopeFactory)
{
    protected override async Task<PersonaCommandResponse> Execute(UpdateArcanaCommand request, CancellationToken cancellationToken)
    {
        var arcana = await dbService.Set<Domain.Entities.Arcana>().Include(a => a.Games).SingleAsync(a => a.Id == request.Id);
        var requested = await dbService.Set<Domain.Entities.Game>()
            .Where(g => request.GameIds.Contains(g.Id))
            .ToListAsync(cancellationToken);

        arcana.Name = request.Name;
        arcana.Games.Replace(requested);
        
        await dbService.SaveChanges(cancellationToken);
        
        return new PersonaCommandResponse();
    }
}

public class UpdateArcanaCommandValidator : AbstractValidator<UpdateArcanaCommand>
{
    private readonly IDbService _dbService;

    public UpdateArcanaCommandValidator(IDbService dbService)
    {
        _dbService = dbService;

        RuleFor(a => a.Id)
            .MustAsync(ArcanaMustExist)
                .WithMessage("You must provide a valid Arcana Id.");

        RuleFor(a => a.Name)
            .NotEmpty()
                .WithMessage("You must provide a name for the Arcana.")
            .MustAsync(NameMustNotBeTaken)
                .WithMessage("That name is already taken.");
        RuleFor(a => a.GameIds)
            .NotEmpty()
                .WithMessage("You must provide at least one game id.")
            .MustAsync(GamesMustExist)
                .WithMessage("Some of the games specified do not exist.");
    }

    private async Task<bool> ArcanaMustExist(int Id, CancellationToken cancellationToken)
    {
        return await _dbService.Set<Domain.Entities.Arcana>().AnyAsync(a => a.Id == Id, cancellationToken);
    }

    private async Task<bool> NameMustNotBeTaken(UpdateArcanaCommand request, string name, CancellationToken cancellationToken)
    {
        var arcana = await _dbService.Set<Domain.Entities.Arcana>().SingleOrDefaultAsync(c => c.Name == name, cancellationToken);

        if (arcana is null)
        {
            return true;
        }

        return arcana.Id == request.Id;
    }

    private async Task<bool> GamesMustExist(IList<int> games, CancellationToken cancellationToken)
    {
        return await _dbService.Set<Domain.Entities.Game>()
            .CountAsync(g => games.Contains(g.Id), cancellationToken) == games.Count;
    }
}