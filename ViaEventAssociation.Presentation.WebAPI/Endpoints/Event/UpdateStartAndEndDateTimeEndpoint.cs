using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.Common.CommandDispatcher;
using ViaEventAssociation.Core.Application.Features.Event.UpdateStartAndEndDateTime;
using ViaEventAssociation.Presentation.WebAPI.Extensions;
using ViaEventAssociation.Presentation.WebAPI.REPRBase;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Event;

public class UpdateStartAndEndDateTimeEndpoint(ICommandDispatcher dispatcher)
    : ApiEndpoint
        .WithRequest<UpdateStartAndEndDateTimeEndpointRequest>
        .WithoutResponse
{
    [HttpPost("event/update-start-end-date")]
    public override async Task<ActionResult> HandleAsync(UpdateStartAndEndDateTimeEndpointRequest request)
    {
        var cmdResult = UpdateEventStartAndEndDateTimeCommand.Create(
            new Guid(request.RequestBody.EventId),
            request.RequestBody.StartDateTime,
            request.RequestBody.EndDateTime
        );

        if (cmdResult.isFailure)
            return cmdResult.ToActionResult(HttpContext);

        var handlerResult = await dispatcher.DispatchAsync(cmdResult.payload);
        if (handlerResult.isFailure)
            return handlerResult.ToActionResult(HttpContext);

        return Ok();
    }
}

public record UpdateStartAndEndDateTimeEndpointRequest(
    [FromBody] UpdateStartAndEndDateTimeEndpointRequest.Body RequestBody
)
{
    public record Body(
        string EventId,
        DateTime StartDateTime,
        DateTime EndDateTime
    );
}
