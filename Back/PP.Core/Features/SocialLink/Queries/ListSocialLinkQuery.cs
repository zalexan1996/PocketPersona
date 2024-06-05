using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PP.Core.Features.Shared.Commands;
using PP.Core.Services;

namespace PP.Core.Features.SocialLink;
public record SocialLinkDto(int Id, string Name, int CharacterId, int ArcanaId, string UnlockConditions, IList<int> DialogueIds);
public record ListSocialLinksQuery(int? Id, int Page = 0, int PageSize = 10) : IRequest<PersonaCommandResponse<IList<SocialLinkDto>>>;

public class ListSocialLinksQueryHandler(IServiceScopeFactory factory, IDbService dbService)
    : PersonaCommandHandler<ListSocialLinksQuery, PersonaCommandResponse<IList<SocialLinkDto>>, ListSocialLinksQueryValidator>(factory)
{
    protected override async Task<PersonaCommandResponse<IList<SocialLinkDto>>> Execute(ListSocialLinksQuery request, CancellationToken cancellationToken)
    {
        var results = await dbService.Set<Domain.Entities.SocialLink>()
            .Include(s => s.Dialogues)
            .Where(s => request.Id == null || s.Id == request.Id)
            .OrderBy(s => s.Name)
            .Skip(request.PageSize * request.Page)
            .Take(request.PageSize)
            .Select(s => new SocialLinkDto(s.Id, s.Name, s.CharacterId, s.ArcanaId, s.UnlockConditions, s.Dialogues.Select(d => d.Id).ToList()))
            .ToListAsync(cancellationToken);

        return new PersonaCommandResponse<IList<SocialLinkDto>>(results);
    }
}

public class ListSocialLinksQueryValidator : AbstractValidator<ListSocialLinksQuery>
{
    private readonly IDbService _dbService;

    public ListSocialLinksQueryValidator(IDbService dbService)
    {
        _dbService = dbService;

        When(x => x.Id is not null, () => {
            RuleFor(x => x.Id)
                .MustAsync(SocialLinkIdMustExist)
                    .WithMessage("The ArcanaId you specified does not exist.");
        });

        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(0)
                .WithMessage("The page number must be greater than or equal to 0.");

        RuleFor(x => x.PageSize)
            .GreaterThan(0)
                .WithMessage("The page size must be greater than 0.");
    }

    private async Task<bool> SocialLinkIdMustExist(int? id, CancellationToken cancellationToken)
    {
        return await _dbService.Set<Domain.Entities.SocialLink>().AnyAsync(a => a.Id == id!, cancellationToken);
    }
}