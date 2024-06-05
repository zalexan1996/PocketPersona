using Microsoft.EntityFrameworkCore;
using PP.Core.Services;

namespace PP.Core.Features.Shared.Services;

public class PersonaValidationLibrary(IDbService dbService)
{
    public async Task<bool> CharacterMustExist(int characterId, CancellationToken cancellationToken)
    {
        return await dbService.Set<Domain.Entities.Character>().AnyAsync(c => c.Id == characterId, cancellationToken);
    }

    public async Task<bool> GameMustExist(int id, CancellationToken cancellationToken)
    {
        return await dbService.Set<Domain.Entities.Game>().AnyAsync(g => g.Id == id, cancellationToken);
    }
    
    public async Task<bool> ArcanaMustExist(int arcanaId, CancellationToken cancellationToken)
    {
        return await dbService.Set<Domain.Entities.Arcana>().AnyAsync(c => c.Id == arcanaId, cancellationToken);
    }
    
    public async Task<bool> SocialLinkMustExist(int socialLinkId, CancellationToken cancellationToken)
    {
        return await dbService.Set<Domain.Entities.SocialLink>().AnyAsync(c => c.Id == socialLinkId, cancellationToken);
    }
    
    public async Task<bool> GameNameNotInUse(string name, CancellationToken cancellationToken)
    {
        return !await dbService.Set<Domain.Entities.Game>().AnyAsync(c => c.Name == name, cancellationToken);
    }

    public async Task<bool> CharacterNameNotInUse(string name, CancellationToken cancellationToken)
    {
        return !await dbService.Set<Domain.Entities.Character>().AnyAsync(c => c.Name == name, cancellationToken);
    }
}