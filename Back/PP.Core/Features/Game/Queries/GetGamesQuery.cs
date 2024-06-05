using MediatR;
using Microsoft.EntityFrameworkCore;
using PP.Core.Features.Models;
using PP.Core.Services;

namespace PP.Core.Features;

public record GetGamesQuery : IRequest<PersonaCommandResponse<IEnumerable<GameDto>>>;

public class GetGamesQueryHandler(IDbService dbService) : IRequestHandler<GetGamesQuery, PersonaCommandResponse<IEnumerable<GameDto>>>
{
    public async Task<PersonaCommandResponse<IEnumerable<GameDto>>> Handle(GetGamesQuery request, CancellationToken cancellationToken)
    {
        var games = await dbService.Set<Domain.Entities.Game>().Select(x => new GameDto(x.Id, x.Name)).ToListAsync(cancellationToken);

        return new PersonaCommandResponse<IEnumerable<GameDto>>(games);
    }
}