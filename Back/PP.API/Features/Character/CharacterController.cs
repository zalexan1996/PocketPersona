using MediatR;
using Microsoft.AspNetCore.Mvc;
using PP.API.Features.Shared;
using PP.Core.Features.Character.Commands;
using PP.Core.Features.Character.Queries;

namespace PP.API.Features.Character;

public record ListCharacterModel(int? GameId, int Page = 0, int PageSize = 20);
public record AddCharacterModel(string Name, int GameId, IList<string> Gifts);
public record UpdateCharacterModel(int Id, int GameId, string Name, IList<string> Gifts);

[ApiController]
[Route("[controller]")]
public class CharacterController(IMediator mediator) : PocketPersonaController(mediator)
{
    [HttpGet]
    public async Task<ActionResult<PersonaCommandResponse<IEnumerable<CharacterDto>>>> List([FromQuery]ListCharacterModel model)
    {
        return await Send<ListCharactersQuery, PersonaCommandResponse<IEnumerable<CharacterDto>>>(new(model.GameId, model.Page, model.PageSize));
    }

    [HttpPost]
    public async Task<ActionResult<PersonaCommandResponse>> Add(AddCharacterModel model)
    {
        return await Send<AddCharacterCommand, PersonaCommandResponse>(new(model.Name, model.GameId, model.Gifts));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<PersonaCommandResponse>> Update(UpdateCharacterModel model)
    {
        return await Send<UpdateCharacterCommand, PersonaCommandResponse>(new(model.Id, model.GameId, model.Name, model.Gifts));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<PersonaCommandResponse>> Delete(int id)
    {
        return await Send<DeleteCharacterCommand, PersonaCommandResponse>(new(id));
    }
}