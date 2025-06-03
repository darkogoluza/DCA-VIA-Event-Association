using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.Common.CommandDispatcher;
using ViaEventAssociation.Core.Application.Features.Invitation.AcceptInvitation;
using ViaEventAssociation.Presentation.WebAPI.Extensions;
using ViaEventAssociation.Presentation.WebAPI.REPRBase;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Invitation;

public class AcceptInvitationEndpoint(ICommandDispatcher dispatcher)
    : ApiEndpoint
        .WithRequest<AcceptInvitationEndpointRequest>
        .WithoutResponse
{
    [HttpPost("invitation/guest-accept")]
    public override async Task<ActionResult> HandleAsync(AcceptInvitationEndpointRequest request)
    {
        var cmdResult = GuestAcceptsInvitationCommand.Create(
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

public record AcceptInvitationEndpointRequest(
    [FromBody] AcceptInvitationEndpointRequest.Body RequestBody
)
{
    public record Body(
        string EventId,
        string GuestId
    );
}
