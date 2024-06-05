using MediatR;
using Microsoft.AspNetCore.Mvc;
using PP.API.Features.Shared;
using PP.Core.Features;
using PP.Core.Features.Commands;
using PP.Core.Features.Game.Commands;
using PP.Core.Features.Models;

namespace PP.API.Features;

public record AddGameModel(string Name);
public record UpdateGameModel(int Id, string Name);

[ApiController]
[Route("[controller]")]
public class GameController(IMediator mediator) : PocketPersonaController(mediator)
{
    [HttpGet]
    public async Task<ActionResult<PersonaCommandResponse<IEnumerable<GameDto>>>> List()
    {
        return await Send<GetGamesQuery, PersonaCommandResponse<IEnumerable<GameDto>>>(new());
    }

    [HttpPost]
    public async Task<ActionResult<PersonaCommandResponse>> Add(AddGameModel model)
    {
        return await Send<AddGameCommand, PersonaCommandResponse>(new(model.Name));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<PersonaCommandResponse>> Update(UpdateGameModel model)
    {
        return await Send<UpdateGameCommand, PersonaCommandResponse>(new(model.Id, model.Name));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<PersonaCommandResponse>> Delete(int id)
    {
        return await Send<DeleteGameCommand, PersonaCommandResponse>(new(id));
    }
}