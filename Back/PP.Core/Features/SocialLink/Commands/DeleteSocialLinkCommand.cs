using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PP.Core.Features.Shared.Commands;
using PP.Core.Features.Shared.Services;
using PP.Core.Services;

namespace PP.Core.Features.SocialLink;

public record DeleteSocialLinkCommand(int Id) : IRequest<PersonaCommandResponse>;

public class DeleteSocialLinkCommandHandler(IServiceScopeFactory factory, IDbService dbService)
    : PersonaCommandHandler<DeleteSocialLinkCommand, PersonaCommandResponse, DeleteSocialLinkCommandValidator>(factory)
{
    protected override async Task<PersonaCommandResponse> Execute(DeleteSocialLinkCommand request, CancellationToken cancellationToken)
    {
        var socialLink = await dbService.Set<Domain.Entities.SocialLink>().SingleAsync(s => s.Id == request.Id);

        dbService.Remove(socialLink);

        await dbService.SaveChanges(cancellationToken);

        return new PersonaCommandResponse();
    }
}
public class DeleteSocialLinkCommandValidator : AbstractValidator<DeleteSocialLinkCommand>
{
    public DeleteSocialLinkCommandValidator(PersonaValidationLibrary library)
    {
        RuleFor(x => x.Id)
            .MustAsync(library.SocialLinkMustExist)
                .WithMessage("You must specify a valid Id.");

    }
}