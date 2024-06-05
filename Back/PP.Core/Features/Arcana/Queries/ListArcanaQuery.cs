using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PP.Core.Features.Shared.Commands;
using PP.Core.Services;

namespace PP.Core.Features.Arcana;

public record ArcanaDto(int Id, string Name, int[] GameIds);

public record ListArcanaQuery(int? Id, int PageNum = 0, int PageSize = 10) : IRequest<PersonaCommandResponse<IEnumerable<ArcanaDto>>>;

public class ListArcanaQueryHandler(IServiceScopeFactory factory, IDbService dbService)
    : PersonaCommandHandler<ListArcanaQuery, PersonaCommandResponse<IEnumerable<ArcanaDto>>, ListArcanaQueryValidator>(factory)
{
    protected override async Task<PersonaCommandResponse<IEnumerable<ArcanaDto>>> Execute(ListArcanaQuery request, CancellationToken cancellationToken)
    {
        var items = await dbService.Set<Domain.Entities.Arcana>()
            .Include(a => a.Games)
            .Where(a => request.Id == null || a.Id == request.Id)
            .OrderBy(a => a.Name)
            .Skip(request.PageSize * request.PageNum)
            .Take(request.PageSize)
            .Select(a => new ArcanaDto(a.Id, a.Name, a.Games.Select(g => g.Id).ToArray()))
            .ToListAsync(cancellationToken);

        return new PersonaCommandResponse<IEnumerable<ArcanaDto>>(items);
    }
}

public class ListArcanaQueryValidator : AbstractValidator<ListArcanaQuery>
{
    private readonly IDbService _dbService;

    public ListArcanaQueryValidator(IDbService dbService)
    {
        _dbService = dbService;

        When(x => x.Id is not null, () => {
            RuleFor(x => x.Id)
                .MustAsync(ArcanaIdMustExist)
                    .WithMessage("The ArcanaId you specified does not exist.");
        });

        RuleFor(x => x.PageNum)
            .GreaterThanOrEqualTo(0)
                .WithMessage("The page number must be greater than or equal to 0.");

        RuleFor(x => x.PageSize)
            .GreaterThan(0)
                .WithMessage("The page size must be greater than 0.");
    }

    private async Task<bool> ArcanaIdMustExist(int? id, CancellationToken cancellationToken)
    {
        return await _dbService.Set<Domain.Entities.Arcana>().AnyAsync(a => a.Id == id!, cancellationToken);
    }
}