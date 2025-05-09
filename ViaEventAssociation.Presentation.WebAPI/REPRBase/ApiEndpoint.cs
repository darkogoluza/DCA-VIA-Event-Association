using Microsoft.AspNetCore.Mvc;

namespace ViaEventAssociation.Presentation.WebAPI.REPRBase;

public static class ApiEndpoint
{
    public static class WithRequest<TResponse> 
    {
        public abstract class WithResponse<TResponse> : EndpointBase
        {
            public abstract Task<ActionResult<TResponse>> HandleAsync();
        }

        public abstract class WithoutResponse : EndpointBase
        {
            public abstract Task<ActionResult> HandleAsync();
        }
    }

    public static class WithoutRequest
    {
        public abstract class WithResponse<TResponse> : EndpointBase
        {
            public abstract Task<ActionResult<TResponse>> HandleAsync();
        }

        public abstract class WithoutResponse : EndpointBase
        {
            public abstract Task<ActionResult> HandleAsync();
        }
    }
}

[ApiController]
[Route("api")]
public abstract class EndpointBase : ControllerBase
{
}
