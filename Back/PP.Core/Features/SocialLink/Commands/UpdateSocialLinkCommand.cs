using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PP.Core.Features.Shared.Commands;
using PP.Core.Features.Shared.Services;
using PP.Core.Services;

namespace PP.Core.Features.SocialLink;

public record UpdateSocialLinkCommand(int Id, string Name, int CharacterId, int ArcanaId, string UnlockConditions) : IRequest<PersonaCommandResponse>;

public class UpdateSocialLinkCommandHandler(IServiceScopeFactory factory, IDbService dbService)
    : PersonaCommandHandler<UpdateSocialLinkCommand, PersonaCommandResponse, UpdateSocialLinkCommandValidator>(factory)
{
    protected override async Task<PersonaCommandResponse> Execute(UpdateSocialLinkCommand request, CancellationToken cancellationToken)
    {
        var socialLink = await dbService.Set<Domain.Entities.SocialLink>().SingleAsync(s => s.Id == request.Id, cancellationToken);

        socialLink.Name = request.Name;
        socialLink.CharacterId = request.CharacterId;
        socialLink.ArcanaId = request.ArcanaId;
        socialLink.UnlockConditions = request.UnlockConditions;

        await dbService.SaveChanges(cancellationToken);

        return new PersonaCommandResponse();
    }
}

public class UpdateSocialLinkCommandValidator :AbstractValidator<UpdateSocialLinkCommand>
{
    public UpdateSocialLinkCommandValidator(PersonaValidationLibrary library)
    {
        RuleFor(x => x.Id)
            .MustAsync(library.SocialLinkMustExist)
                .WithMessage("The specified social link must exist.");

        RuleFor(x => x.CharacterId)
            .MustAsync(library.CharacterMustExist)
                .WithMessage("The specified character must exist.");

        RuleFor(x => x.ArcanaId)
            .MustAsync(library.ArcanaMustExist)
                .WithMessage("The specified arcana must exist.");

        RuleFor(x => x.UnlockConditions)
            .NotEmpty()
                .WithMessage("You must specify the unlock conditions.");
                
        RuleFor(x => x.Name)
            .NotEmpty()
                .WithMessage("You must specify the name.");
    }
}