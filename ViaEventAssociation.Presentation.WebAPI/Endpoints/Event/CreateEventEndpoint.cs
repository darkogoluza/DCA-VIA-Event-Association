using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.Common.CommandDispatcher;
using ViaEventAssociation.Core.Application.Features.Event.CreateEvent;
using ViaEventAssociation.Presentation.WebAPI.Extensions;
using ViaEventAssociation.Presentation.WebAPI.REPRBase;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Event;

public class CreateEventEndpoint(ICommandDispatcher dispatcher) : ApiEndpoint.WithoutRequest.WithResponse<OkResult>
{
    [HttpPost("event/create")]
    public override async Task<ActionResult<OkResult>> HandleAsync()
    {
        var cmdResult = CreateEventCommand.Create();

        if (cmdResult.isFailure)
            return cmdResult.ToActionResult(HttpContext);

        var handlerResult = await dispatcher.DispatchAsync(cmdResult);

        if (handlerResult.isFailure)
            return handlerResult.ToActionResult(HttpContext);

        return new OkResult();
    }
}
