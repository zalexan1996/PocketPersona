using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PP.Core.Features.Shared.Commands;
using PP.Core.Features.Shared.Services;
using PP.Core.Services;

namespace PP.Core.Features.Character.Commands;

public record DeleteCharacterCommand(int Id) : IRequest<PersonaCommandResponse>;

public class DeleteCharacterCommandHandler(IServiceScopeFactory scopeFactory, IDbService dbService)
    : PersonaCommandHandler<DeleteCharacterCommand, PersonaCommandResponse, DeleteCharacterCommandValidator>(scopeFactory)
{
    protected override async Task<PersonaCommandResponse> Execute(DeleteCharacterCommand request, CancellationToken cancellationToken)
    {
        var character = await dbService.Set<Domain.Entities.Character>().SingleAsync(c => c.Id == request.Id, cancellationToken);
        dbService.Remove(character);
        await dbService.SaveChanges(cancellationToken);

        return new PersonaCommandResponse();
    }
}
public class DeleteCharacterCommandValidator : AbstractValidator<DeleteCharacterCommand>
{
    public DeleteCharacterCommandValidator(PersonaValidationLibrary library)
    {
        RuleFor(x => x.Id)
            .MustAsync(library.CharacterMustExist)
                .WithMessage("The specified Id did not belong to a valid character.");
    }
}