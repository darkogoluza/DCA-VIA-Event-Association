using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.Common.CommandDispatcher;
using ViaEventAssociation.Core.Application.Features.Event.GuestCancelsParticipationEvent;
using ViaEventAssociation.Presentation.WebAPI.Extensions;
using ViaEventAssociation.Presentation.WebAPI.REPRBase;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Event;

public class GuestCancelsParticipationEventEndpoint(ICommandDispatcher dispatcher)
    : ApiEndpoint
        .WithRequest<GuestCancelsParticipationEventEndpointRequest>
        .WithoutResponse
{
    [HttpPost("event/guest-cancels-participation")]
    public override async Task<ActionResult> HandleAsync(GuestCancelsParticipationEventEndpointRequest request)
    {
        var cmdResult = GuestCancelsParticipationEventCommand.Create(
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

public record GuestCancelsParticipationEventEndpointRequest(
    [FromBody] GuestCancelsParticipationEventEndpointRequest.Body RequestBody
)
{
    public record Body(
        string EventId,
        string GuestId
    );
}
