using MediatR;
using Microsoft.AspNetCore.Mvc;
using PP.API.Features.Shared;
using PP.Core.Features.Arcana;

namespace PP.API.Features.Arcana;

public record ListArcanaModel(int? Id, int Page = 0, int PageSize = 10);
public record AddArcanaModel(string Name, IList<int> GameIds);

[ApiController]
[Route("[controller]")]
public class ArcanaController(IMediator mediator) : PocketPersonaController(mediator)
{
    [HttpGet]
    public async Task<ActionResult<PersonaCommandResponse<IEnumerable<ArcanaDto>>>> List([FromQuery]ListArcanaModel model)
    {
        return await Send<ListArcanaQuery, PersonaCommandResponse<IEnumerable<ArcanaDto>>>(new ListArcanaQuery(model.Id, model.Page, model.PageSize));
    }

    [HttpPost]
    public async Task<ActionResult<PersonaCommandResponse>> Add(AddArcanaModel model)
    {
        return await Send<AddArcanaCommand, PersonaCommandResponse>(new AddArcanaCommand(model.Name, model.GameIds));
    }

    [HttpPut]
    public async Task<ActionResult<PersonaCommandResponse>> Update(int id, AddArcanaModel model)
    {
        return await Send<UpdateArcanaCommand, PersonaCommandResponse>(new UpdateArcanaCommand(id, model.Name, model.GameIds));
    }
    
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<PersonaCommandResponse>> Delete(int id)
    {
        return await Send<DeleteArcanaCommand, PersonaCommandResponse>(new DeleteArcanaCommand(id));
    }
}