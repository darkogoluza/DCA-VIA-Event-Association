using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.Common.CommandDispatcher;
using ViaEventAssociation.Core.Application.Features.Event.UpdateVisibility;
using ViaEventAssociation.Presentation.WebAPI.Extensions;
using ViaEventAssociation.Presentation.WebAPI.REPRBase;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Event;

public class UpdateVisibilityEndpoint(ICommandDispatcher dispatcher)
    : ApiEndpoint
        .WithRequest<UpdateVisibilityEndpointRequest>
        .WithoutResponse
{
    [HttpPost("event/update-visibility")]
    public override async Task<ActionResult> HandleAsync(UpdateVisibilityEndpointRequest request)
    {
        var cmdResult = UpdateEventVisibilityCommand.Create(
            new Guid(request.RequestBody.EventId),
            request.RequestBody.Visibility
        );

        if (cmdResult.isFailure)
            return cmdResult.ToActionResult(HttpContext);

        var handlerResult = await dispatcher.DispatchAsync(cmdResult.payload);
        if (handlerResult.isFailure)
            return handlerResult.ToActionResult(HttpContext);

        return Ok();
    }
}

public record UpdateVisibilityEndpointRequest(
    [FromBody] UpdateVisibilityEndpointRequest.Body RequestBody
)
{
    public record Body(
        string EventId,
        bool Visibility
    );
}
