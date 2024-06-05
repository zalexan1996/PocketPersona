using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PP.Core.Features.Shared.Commands;
using PP.Core.Services;

namespace PP.Core.Features.SocialLink;
public record SocialLinkDialogueDto(int Id, int SocialLinkId, string Text, int Rank, int Order);
public record ListSocialLinkDialogueQuery(int? Id, int? SocialLinkId) : IRequest<PersonaCommandResponse<IList<SocialLinkDialogueDto>>>;

public class ListSocialLinkDialogueQueryHandler(IServiceScopeFactory factory, IDbService dbService)
    : PersonaCommandHandler<ListSocialLinkDialogueQuery, PersonaCommandResponse<IList<SocialLinkDialogueDto>>, ListSocialLinkDialogueQueryValidator>(factory)
{
    protected override async Task<PersonaCommandResponse<IList<SocialLinkDialogueDto>>> Execute(ListSocialLinkDialogueQuery request, CancellationToken cancellationToken)
    {
        var records = await dbService.Set<Domain.Entities.SocialLinkDialogue>()
            .Where(x => request.Id == null || x.Id == request.Id)
            .Where(x => request.SocialLinkId == null || x.SocialLinkId == request.SocialLinkId)
            .OrderBy(x => x.SocialLinkId).ThenBy(x => x.Rank).ThenBy(x => x.Order)
            .Select(x => new SocialLinkDialogueDto(x.Id, x.SocialLinkId, x.Text, x.Rank, x.Order))
            .ToListAsync(cancellationToken);

        return new PersonaCommandResponse<IList<SocialLinkDialogueDto>>(records);
    }
}

public class ListSocialLinkDialogueQueryValidator : AbstractValidator<ListSocialLinkDialogueQuery>
{
    private readonly IDbService _dbService;

    public ListSocialLinkDialogueQueryValidator(IDbService dbService)
    {
        _dbService = dbService;

        When(x => x.Id != null, () => {
            RuleFor(x => x.Id)
                .MustAsync(DialogueMustExist);
        });

        When(x => x.SocialLinkId != null, () => {
            RuleFor(x => x.SocialLinkId)
                .MustAsync(SocialLinkMustExist);
        });
    }

    private async Task<bool> SocialLinkMustExist(int? socialLinkId, CancellationToken cancellationToken)
    {
        return await _dbService.Set<Domain.Entities.SocialLink>().AnyAsync(c => c.Id == socialLinkId!, cancellationToken);
    }

    private async Task<bool> DialogueMustExist(int? dialogue, CancellationToken cancellationToken)
    {
        return await _dbService.Set<Domain.Entities.SocialLinkDialogue>().AnyAsync(d => d.Id == dialogue!, cancellationToken);
    }
}