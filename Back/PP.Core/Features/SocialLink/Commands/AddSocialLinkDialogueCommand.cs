using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PP.Core.Features.Shared.Commands;
using PP.Core.Features.Shared.Services;
using PP.Core.Services;

namespace PP.Core.Features.SocialLink;

public record AddSocialLinkDialogueCommand(int SocialLinkId, string Text, int Rank, int Order) : IRequest<PersonaCommandResponse>;

public class AddSocialLinkDialogueCommandValidator : AbstractValidator<AddSocialLinkDialogueCommand>
{
    public AddSocialLinkDialogueCommandValidator(PersonaValidationLibrary library)
    {
        RuleFor(x => x.SocialLinkId)
            .MustAsync(library.SocialLinkMustExist)
                .WithMessage("The specified SocialLinkId is not valid.");

        RuleFor(x => x.Text)
            .NotEmpty()
                .WithMessage("You must provide the Text for the dialogue.");

        RuleFor(x => x.Rank)
            .GreaterThan(0).WithMessage("The rank must be at least 1.")
            .LessThanOrEqualTo(10).WithMessage("The rank must not be greater than 10.");
    }
}

public class AddSocialLinkDialogueCommandHandler(IServiceScopeFactory factory, IDbService dbService)
    : PersonaCommandHandler<AddSocialLinkDialogueCommand, PersonaCommandResponse, AddSocialLinkDialogueCommandValidator>(factory)
{
    protected override async Task<PersonaCommandResponse> Execute(AddSocialLinkDialogueCommand request, CancellationToken cancellationToken)
    {
        var socialLink = await dbService.Set<Domain.Entities.SocialLink>().Include(x => x.Dialogues).SingleAsync(s => s.Id == request.SocialLinkId);

        socialLink.Dialogues.Add(new Domain.Entities.SocialLinkDialogue()
        {
            SocialLinkId = request.SocialLinkId,
            Text = request.Text,
            Order = request.Order,
            Rank = request.Rank,
        });

        await dbService.SaveChanges(cancellationToken);

        return new PersonaCommandResponse();
    }
}