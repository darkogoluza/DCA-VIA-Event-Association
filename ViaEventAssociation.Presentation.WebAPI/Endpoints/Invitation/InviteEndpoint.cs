using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.Common.CommandDispatcher;
using ViaEventAssociation.Core.Application.Features.Invitation.Invate;
using ViaEventAssociation.Presentation.WebAPI.Extensions;
using ViaEventAssociation.Presentation.WebAPI.REPRBase;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Invitation;

public class InviteEndpoint(ICommandDispatcher dispatcher)
    : ApiEndpoint
        .WithRequest<InviteEndpointRequest>
        .WithoutResponse
{
    [HttpPost("invitation/invite")]
    public override async Task<ActionResult> HandleAsync(InviteEndpointRequest request)
    {
        var cmdResult = GuestInvitedCommand.Create(
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

public record InviteEndpointRequest(
    [FromBody] InviteEndpointRequest.Body RequestBody
)
{
    public record Body(
        string EventId,
        string GuestId
    );
}
