using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.Common.CommandDispatcher;
using ViaEventAssociation.Core.Application.Features.Event.UpdateDescription;
using ViaEventAssociation.Presentation.WebAPI.Extensions;
using ViaEventAssociation.Presentation.WebAPI.REPRBase;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Event;

public class UpdateDescriptionEndpoint(ICommandDispatcher dispatcher)
: ApiEndpoint
    .WithRequest<UpdateDescriptionEndpointRequest>
    .WithoutResponse
{
    [HttpPost("event/update-description")]
    public override async Task<ActionResult> HandleAsync(UpdateDescriptionEndpointRequest request)
    {
        var cmdResult = UpdateEventDescriptionCommand.Create(
            new Guid(request.RequestBody.EventId),
            request.RequestBody.Description
        );

        if (cmdResult.isFailure)
            return cmdResult.ToActionResult(HttpContext);

        var handlerResult = await dispatcher.DispatchAsync(cmdResult.payload);
        if (handlerResult.isFailure)
            return handlerResult.ToActionResult(HttpContext);

        return Ok();
    }
}

public record UpdateDescriptionEndpointRequest(
    [FromBody] UpdateDescriptionEndpointRequest.Body RequestBody
)
{
    public record Body(
        string EventId,
        string Description
    );
}
