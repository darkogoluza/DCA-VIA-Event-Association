using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.Common.CommandDispatcher;
using ViaEventAssociation.Core.Application.Features.Event.UpdateTitle;
using ViaEventAssociation.Presentation.WebAPI.Extensions;
using ViaEventAssociation.Presentation.WebAPI.REPRBase;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Event;

public class UpdateTitleEndpoint(ICommandDispatcher dispatcher)
    : ApiEndpoint
        .WithRequest<UpdateTitleEndpointRequest>
        .WithoutResponse
{
    [HttpPost("event/update-title")]
    public override async Task<ActionResult> HandleAsync(UpdateTitleEndpointRequest request)
    {
        var cmdResult = UpdateEventTitleCommand.Create(
            new Guid(request.RequestBody.EventId),
            request.RequestBody.Title
        );

        if (cmdResult.isFailure)
            return cmdResult.ToActionResult(HttpContext);

        var handlerResult = await dispatcher.DispatchAsync(cmdResult.payload);
        if (handlerResult.isFailure)
            return handlerResult.ToActionResult(HttpContext);

        return Ok();
    }
}

public record UpdateTitleEndpointRequest(
    [FromBody] UpdateTitleEndpointRequest.Body RequestBody
)
{
    public record Body(
        string EventId,
        string Title
    );
}
