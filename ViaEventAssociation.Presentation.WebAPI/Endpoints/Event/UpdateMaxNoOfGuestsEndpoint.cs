using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.Common.CommandDispatcher;
using ViaEventAssociation.Core.Application.Features.Event.UpdateMaxNoOfGuests;
using ViaEventAssociation.Presentation.WebAPI.Extensions;
using ViaEventAssociation.Presentation.WebAPI.REPRBase;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Event;

public class UpdateMaxNoOfGuestsEndpoint(ICommandDispatcher dispatcher)
    : ApiEndpoint
        .WithRequest<UpdateMaxNoOfGuestsEndpointRequest>
        .WithoutResponse
{
    [HttpPost("event/update-max-number-of-guests")]
    public override async Task<ActionResult> HandleAsync(UpdateMaxNoOfGuestsEndpointRequest request)
    {
        var cmdResult = UpdateEventMaxNoOfGuestsCommand.Create(
            new Guid(request.RequestBody.EventId),
            request.RequestBody.MaxNoOfGuests
        );

        if (cmdResult.isFailure)
            return cmdResult.ToActionResult(HttpContext);

        var handlerResult = await dispatcher.DispatchAsync(cmdResult.payload);
        if (handlerResult.isFailure)
            return handlerResult.ToActionResult(HttpContext);

        return Ok();
    }
}

public record UpdateMaxNoOfGuestsEndpointRequest(
    [FromBody] UpdateMaxNoOfGuestsEndpointRequest.Body RequestBody
)
{
    public record Body(
        string EventId,
        int MaxNoOfGuests
    );
}
