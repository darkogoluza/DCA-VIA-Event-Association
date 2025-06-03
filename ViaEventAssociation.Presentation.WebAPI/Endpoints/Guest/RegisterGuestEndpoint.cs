using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.Common.CommandDispatcher;
using ViaEventAssociation.Core.Application.Features.Guest.RegisterGuest;
using ViaEventAssociation.Presentation.WebAPI.Extensions;
using ViaEventAssociation.Presentation.WebAPI.REPRBase;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Guest;

public class RegisterGuestEndpoint(ICommandDispatcher dispatcher)
    : ApiEndpoint
        .WithRequest<RegisterGuestEndpointRequest>
        .WithoutResponse
{
    [HttpPost("guest/register")]
    public override async Task<ActionResult> HandleAsync(RegisterGuestEndpointRequest request)
    {
        var cmdResult = RegisterGuestCommand.Create(
            request.RequestBody.FirstName,
            request.RequestBody.LastName,
            request.RequestBody.Email,
            request.RequestBody.ProfilePictureUrl
        );

        if (cmdResult.isFailure)
            return cmdResult.ToActionResult(HttpContext);

        var handlerResult = await dispatcher.DispatchAsync(cmdResult.payload);
        if (handlerResult.isFailure)
            return handlerResult.ToActionResult(HttpContext);

        return Ok();
    }
}

public record RegisterGuestEndpointRequest(
    [FromBody] RegisterGuestEndpointRequest.Body RequestBody
)
{
    public record Body(
        string FirstName,
        string LastName,
        string Email,
        string ProfilePictureUrl
    );
}
