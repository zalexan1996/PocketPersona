using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PP.API.Features.Shared;

public class PocketPersonaController(IMediator mediator) : ControllerBase
{
    public async Task<ActionResult<TResponse>> Send<TRequest, TResponse>(TRequest command)
        where TRequest : IRequest<TResponse>
        where TResponse : PersonaCommandResponse
    {
        var response = await mediator.Send(command);

        if (!response.IsValid)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }
}