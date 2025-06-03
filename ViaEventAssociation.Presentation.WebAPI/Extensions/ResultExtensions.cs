using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Presentation.WebAPI.Extensions;

public static class ResultExtensions
{
    public static ActionResult ToActionResult<T>(this Result<T> result, HttpContext context)
    {
        if (result.isFailure)
        {
            context.Response.StatusCode = result.errors.First().code;
            return new JsonResult(new
            {
                success = false,
                errors = result.errors.Select(e => new { e.code, e.message })
            });
        }

        return new OkResult();
    }
}
