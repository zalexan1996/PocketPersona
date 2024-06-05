using MediatR;
using Microsoft.EntityFrameworkCore;
using PP.Core.Services;
using PP.Domain.Entities;

namespace PP.Core.Features.Character.Queries;
public record CharacterDto(int Id, string Name, ICollection<string> Gifts, int GameId);
public record ListCharactersQuery(int? GameId, int Page = 0, int PageSize = 20) : IRequest<PersonaCommandResponse<IEnumerable<CharacterDto>>>;

public class ListCharactersQueryHandler(IDbService dbService) : IRequestHandler<ListCharactersQuery, PersonaCommandResponse<IEnumerable<CharacterDto>>>
{
    public async Task<PersonaCommandResponse<IEnumerable<CharacterDto>>> Handle(ListCharactersQuery request, CancellationToken cancellationToken)
    {
        var characters = await dbService.Set<Domain.Entities.Character>()
            .Where(c => request.GameId == null || c.GameId == request.GameId)
            .OrderBy(c => c.Name)
            .Skip(request.Page * request.PageSize)
            .Take(request.PageSize)
            .Select(c => new CharacterDto(c.Id, c.Name, c.Gifts, c.GameId))
            .ToListAsync(cancellationToken);

        return new PersonaCommandResponse<IEnumerable<CharacterDto>>(characters);
    }
}