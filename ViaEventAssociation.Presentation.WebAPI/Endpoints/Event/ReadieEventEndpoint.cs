using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.Common.CommandDispatcher;
using ViaEventAssociation.Core.Application.Features.Event.ReadieEvent;
using ViaEventAssociation.Presentation.WebAPI.Extensions;
using ViaEventAssociation.Presentation.WebAPI.REPRBase;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Event;

public class ReadieEventEndpoint(ICommandDispatcher dispatcher)
    : ApiEndpoint
        .WithRequest<ReadieEventEndpointRequest>
        .WithoutResponse
{
    [HttpPost("event/readie")]
    public override async Task<ActionResult> HandleAsync(ReadieEventEndpointRequest request)
    {
        var cmdResult = ReadieEventCommand.Create(
            new Guid(request.RequestBody.EventId)
        );

        if (cmdResult.isFailure)
            return cmdResult.ToActionResult(HttpContext);

        var handlerResult = await dispatcher.DispatchAsync(cmdResult.payload);
        if (handlerResult.isFailure)
            return handlerResult.ToActionResult(HttpContext);

        return Ok();
    }
}

public record ReadieEventEndpointRequest(
    [FromBody] ReadieEventEndpointRequest.Body RequestBody
)
{
    public record Body(
        string EventId
    );
}
