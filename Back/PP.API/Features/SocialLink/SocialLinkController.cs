using MediatR;
using Microsoft.AspNetCore.Mvc;
using PP.API.Features.Shared;
using PP.Core.Features.SocialLink;

namespace PP.API.Features.SocialLink;

public record ListSocialLinkModel(int? Id, int Page = 0, int PageSize = 10);
public record AddSocialLinkModel(string Name, int CharacterId, int ArcanaId, string UnlockConditions = "");
public record AddSocialLinkDialogueModel(string Text, int Rank, int Order);

[ApiController]
[Route("[controller]")]
public class SocialLinkController(IMediator mediator) : PocketPersonaController(mediator)
{
    [HttpGet()]
    public async Task<ActionResult<PersonaCommandResponse<IList<SocialLinkDto>>>> List([FromQuery]ListSocialLinkModel model)
    {
        return await Send<ListSocialLinksQuery, PersonaCommandResponse<IList<SocialLinkDto>>>(new ListSocialLinksQuery(model.Id, model.Page, model.PageSize));
    }
    
    [HttpPost()]
    public async Task<ActionResult<PersonaCommandResponse>> Add(AddSocialLinkModel model)
    {
        return await Send<AddSocialLinkCommand, PersonaCommandResponse>(new AddSocialLinkCommand(model.Name, model.CharacterId, model.ArcanaId, model.UnlockConditions));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<PersonaCommandResponse>> Update(int id, AddSocialLinkModel model)
    {
        return await Send<UpdateSocialLinkCommand, PersonaCommandResponse>(new UpdateSocialLinkCommand(id, model.Name, model.CharacterId, model.ArcanaId, model.UnlockConditions));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<PersonaCommandResponse>> Delete(int id)
    {
        return await Send<DeleteSocialLinkCommand, PersonaCommandResponse>(new DeleteSocialLinkCommand(id));
    }

    [HttpGet("{socialLinkId:int}/dialogue")]
    public async Task<ActionResult<PersonaCommandResponse<IList<SocialLinkDialogueDto>>>> ListDialogue(int socialLinkId)
    {
        return await Send<ListSocialLinkDialogueQuery, PersonaCommandResponse<IList<SocialLinkDialogueDto>>>(new ListSocialLinkDialogueQuery(null, socialLinkId));
    }

    [HttpGet("dialogue")]
    public async Task<ActionResult<PersonaCommandResponse<IList<SocialLinkDialogueDto>>>> ListDialogue()
    {
        return await Send<ListSocialLinkDialogueQuery, PersonaCommandResponse<IList<SocialLinkDialogueDto>>>(new ListSocialLinkDialogueQuery(null, null));
    }

    [HttpPost("{socialLinkId:int}/dialogue")]
    public async Task<ActionResult<PersonaCommandResponse>> AddDialogue(int socialLinkId, AddSocialLinkDialogueModel model)
    {
        return await Send<AddSocialLinkDialogueCommand, PersonaCommandResponse>(new AddSocialLinkDialogueCommand(socialLinkId, model.Text, model.Rank, model.Order));
    }

    [HttpDelete("dialogue/{dialogueId:int}")]
    public async Task<ActionResult<PersonaCommandResponse>> DeleteDialogue(int dialogueId)
    {
        return await Send<DeleteSocialLinkDialogueCommand, PersonaCommandResponse>(new DeleteSocialLinkDialogueCommand(dialogueId));
    }
}