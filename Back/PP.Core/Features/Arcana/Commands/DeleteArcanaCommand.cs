using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PP.Core.Features.Shared.Commands;
using PP.Core.Services;

namespace PP.Core.Features.Arcana;

public record DeleteArcanaCommand(int Id) : IRequest<PersonaCommandResponse>;

public class DeleteArcanaCommandHandler(IServiceScopeFactory factory, IDbService dbService) : PersonaCommandHandler<DeleteArcanaCommand, PersonaCommandResponse, DeleteArcanaCommandValidator>(factory)
{
    protected override async Task<PersonaCommandResponse> Execute(DeleteArcanaCommand request, CancellationToken cancellationToken)
    {
        var arcana = await dbService.Set<Domain.Entities.Arcana>().SingleAsync(a => a.Id == request.Id, cancellationToken);

        dbService.Remove(arcana);

        await dbService.SaveChanges(cancellationToken);

        return new PersonaCommandResponse();
    }
}

public class DeleteArcanaCommandValidator : AbstractValidator<DeleteArcanaCommand>
{
    private readonly IDbService _dbService;
    public DeleteArcanaCommandValidator(IDbService dbService)
    {
        _dbService = dbService;

        RuleFor(a => a.Id)
            .MustAsync(BeValidArcana)
                .WithMessage("The Id provided does not belong to a valid Arcana.");
    }

    private async Task<bool> BeValidArcana(int id, CancellationToken cancellationToken)
    {
        return await _dbService.Set<Domain.Entities.Arcana>().AnyAsync(a => a.Id == id, cancellationToken);
    }
}