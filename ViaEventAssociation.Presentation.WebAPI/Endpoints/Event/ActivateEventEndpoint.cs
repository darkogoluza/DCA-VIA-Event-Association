using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.Common.CommandDispatcher;
using ViaEventAssociation.Core.Application.Features.Event.ActivateEvent;
using ViaEventAssociation.Presentation.WebAPI.Extensions;
using ViaEventAssociation.Presentation.WebAPI.REPRBase;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Event;

public class ActivateEventEndpoint(ICommandDispatcher dispatcher)
    : ApiEndpoint
        .WithRequest<ActivateEventEndpointRequest>
        .WithoutResponse
{
    [HttpPost("event/activate")]
    public override async Task<ActionResult> HandleAsync(ActivateEventEndpointRequest request)
    {
        var cmdResult = ActivateEventCommand.Create(
            new Guid(request.RequestBody.Id)
        );

        if (cmdResult.isFailure)
            return cmdResult.ToActionResult(HttpContext);

        var handleResult = await dispatcher.DispatchAsync(cmdResult.payload);
        if (handleResult.isFailure)
            return cmdResult.ToActionResult(HttpContext);

        return Ok();
    }
}

public record ActivateEventEndpointRequest(
    [FromBody] ActivateEventEndpointRequest.ActivateEventBody RequestBody
)
{
    public record ActivateEventBody(
        string Id
    );
}
