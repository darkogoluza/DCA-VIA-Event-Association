using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.Common.CommandDispatcher;
using ViaEventAssociation.Core.Application.Features.Invitation.DeclineInvitation;
using ViaEventAssociation.Presentation.WebAPI.Extensions;
using ViaEventAssociation.Presentation.WebAPI.REPRBase;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Invitation;

public class DeclineInvitationEndpoint(ICommandDispatcher dispatcher)
    : ApiEndpoint
        .WithRequest<DeclineInvitationEndpointRequest>
        .WithoutResponse
{
    [HttpPost("invitation/guest-decline")]
    public override async Task<ActionResult> HandleAsync(DeclineInvitationEndpointRequest request)
    {
        var cmdResult = GuestDeclineInvitationCommand.Create(
            new Guid(request.RequestBody.EventId),
            new Guid(request.RequestBody.GuestId)
        );

        if (cmdResult.isFailure)
            return cmdResult.ToActionResult(HttpContext);

        var handlerResult = await dispatcher.DispatchAsync(cmdResult.payload);
        if (handlerResult.isFailure)
            return handlerResult.ToActionResult(HttpContext);

        return Ok();
    }
}

public record DeclineInvitationEndpointRequest(
    [FromBody] DeclineInvitationEndpointRequest.DeclineInvitationBody RequestBody
)
{
    public record DeclineInvitationBody(
        string EventId,
        string GuestId
    );
}
