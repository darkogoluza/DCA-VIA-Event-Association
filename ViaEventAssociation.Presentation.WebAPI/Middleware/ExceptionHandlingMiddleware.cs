using System.Text.Json;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Presentation.WebAPI.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context); // proceed to next middleware / endpoint
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred.");

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var error = Error.Default();

            var json = JsonSerializer.Serialize(new
            {
                success = false,
                errors = new[] { new { error.code, error.message } }
            });

            await context.Response.WriteAsync(json);
        }
    }
}
