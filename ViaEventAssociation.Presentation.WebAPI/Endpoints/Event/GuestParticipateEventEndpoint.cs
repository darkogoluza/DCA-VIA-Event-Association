using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.Common.CommandDispatcher;
using ViaEventAssociation.Core.Application.Features.Event.GuestParticipateEvent;
using ViaEventAssociation.Presentation.WebAPI.Extensions;
using ViaEventAssociation.Presentation.WebAPI.REPRBase;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Event;

public class GuestParticipateEventEndpoint(ICommandDispatcher dispatcher)
    : ApiEndpoint
        .WithRequest<GuestParticipateEventEndpointRequest>
        .WithoutResponse
{
    [HttpPost("event/guest-participate-event")]
    public override async Task<ActionResult> HandleAsync(GuestParticipateEventEndpointRequest request)
    {
        var cmdResult = GuestParticipateEventCommand.Create(
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

public record GuestParticipateEventEndpointRequest(
    [FromBody] GuestParticipateEventEndpointRequest.Body RequestBody
)
{
    public record Body(
        string EventId,
        string GuestId
    );
}
