using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PP.Core.Features.Shared.Commands;
using PP.Core.Features.Shared.Services;
using PP.Core.Services;

namespace PP.Core.Features.SocialLink;

public record AddSocialLinkCommand(string Name, int CharacterId, int ArcanaId, string UnlockConditions) : IRequest<PersonaCommandResponse>;

public class AddSocialLinkCommandHandler(IServiceScopeFactory factory, IDbService dbService) : PersonaCommandHandler<AddSocialLinkCommand, PersonaCommandResponse, AddSocialLinkCommandValidator>(factory)
{
    protected override async Task<PersonaCommandResponse> Execute(AddSocialLinkCommand request, CancellationToken cancellationToken)
    {
        dbService.Add(new Domain.Entities.SocialLink()
        {
            Name = request.Name,
            CharacterId = request.CharacterId,
            ArcanaId = request.ArcanaId,
            UnlockConditions = request.UnlockConditions
        });

        await dbService.SaveChanges(cancellationToken);
        
        return new PersonaCommandResponse();
    }
}

public class AddSocialLinkCommandValidator : AbstractValidator<AddSocialLinkCommand>
{
    public AddSocialLinkCommandValidator(PersonaValidationLibrary library)
    {
        RuleFor(x => x.ArcanaId)
            .MustAsync(library.ArcanaMustExist)
                .WithMessage("The ArcanaId you specified is not valid.");

        RuleFor(x => x.UnlockConditions)
            .NotEmpty()
                .WithMessage("You must specify the unlock conditions.");

        RuleFor(x => x.CharacterId)
            .MustAsync(library.CharacterMustExist)
                .WithMessage("The CharacterId you specified is not valid.");

        RuleFor(x => x.Name)
            .NotEmpty()
                .WithMessage("You must provide a name.");
    }

}