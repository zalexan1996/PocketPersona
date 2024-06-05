using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PP.Core.Features.Shared.Commands;
using PP.Core.Services;
using System.Collections.Generic;

namespace PP.Core.Features.SocialLink;

public record DeleteSocialLinkDialogueCommand(int Id) : IRequest<PersonaCommandResponse>;

public class DeleteSocialLinkDialogueCommandValidator : AbstractValidator<DeleteSocialLinkDialogueCommand>
{
    private readonly IDbService _dbService;
    public DeleteSocialLinkDialogueCommandValidator(IDbService dbService)
    {
        _dbService = dbService;

        RuleFor(x => x.Id)
            .MustAsync(DialogueMustExist)
                .WithMessage("You must specify a valid dialogue id.");
    }

    private async Task<bool> DialogueMustExist(int id, CancellationToken cancellationToken)
    {
        return await _dbService.Set<Domain.Entities.SocialLinkDialogue>().AnyAsync(x => x.Id == id);
    }
}

public class DeleteSocialLinkDialogueCommandHandler(IServiceScopeFactory factory, IDbService dbService)
    : PersonaCommandHandler<DeleteSocialLinkDialogueCommand, PersonaCommandResponse, DeleteSocialLinkDialogueCommandValidator>(factory)
{
    protected override async Task<PersonaCommandResponse> Execute(DeleteSocialLinkDialogueCommand request, CancellationToken cancellationToken)
    {
        var dialogue = await dbService.Set<Domain.Entities.SocialLinkDialogue>().SingleAsync(x => x.Id == request.Id);
        var socialLinkId = dialogue.SocialLinkId;
        var order = dialogue.Order;
        dbService.Remove(dialogue);

        var socialLink = await dbService.Set<Domain.Entities.SocialLink>().Include(x => x.Dialogues).SingleAsync(x => x.Id == socialLinkId);

        foreach (var d in socialLink.Dialogues.Where(x => x.Order > order))
        {
            d.Order = --d.Order;
        }


        await dbService.SaveChanges(cancellationToken);

        return new PersonaCommandResponse();
    }
}