using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PP.Core.Features.Shared.Commands;
using PP.Core.Services;

namespace PP.Core.Features.Arcana;

public record AddArcanaCommand(string Name, IList<int> GameIds) : IRequest<PersonaCommandResponse>;

public class AddArcanaCommandHandler(IServiceScopeFactory scopeFactory, IDbService dbService)
    : PersonaCommandHandler<AddArcanaCommand, PersonaCommandResponse, AddArcanaCommandValidator>(scopeFactory)
{
    protected override async Task<PersonaCommandResponse> Execute(AddArcanaCommand request, CancellationToken cancellationToken)
    {
        dbService.Add(new Domain.Entities.Arcana()
        {
            Name = request.Name,
            Games = await dbService.Set<Domain.Entities.Game>()
                .Where(g => request.GameIds.Contains(g.Id))
                .ToListAsync(cancellationToken)
        });

        await dbService.SaveChanges(cancellationToken);

        return new PersonaCommandResponse();
    }
}

public class AddArcanaCommandValidator : AbstractValidator<AddArcanaCommand>
{
    private readonly IDbService _dbService;
    public AddArcanaCommandValidator(IDbService dbService)
    {
        _dbService = dbService;

        RuleFor(a => a.Name)
            .NotEmpty()
                .WithMessage("You must provide a value for the name.")
            .MustAsync(ArcanaNameNotBeUsed)
                .WithMessage("That Arcana name is already in use.");

        RuleFor(a => a.GameIds)
            .NotEmpty()
                .WithMessage("You must provide at least one game id.")
            .MustAsync(AllGamesMustExist)
                .WithMessage("You've provided a game id that doesn't exist.");
    }

    private async Task<bool> ArcanaNameNotBeUsed(string name, CancellationToken cancellationToken)
    {
        return !await _dbService.Set<Domain.Entities.Arcana>().AnyAsync(a => a.Name == name);
    }

    private async Task<bool> AllGamesMustExist(IList<int> game, CancellationToken cancellationToken)
    {
        return await _dbService.Set<Domain.Entities.Game>().CountAsync(g => game.Contains(g.Id)) == game.Count;
    }
}