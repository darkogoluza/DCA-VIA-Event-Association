using System.Runtime.InteropServices.JavaScript;

namespace ViaEventAssociation.Core.Tools.OperationResult;

public class Error
{
    public int code { get; }
    public string message { get; }

    public Error(string message, int code)
    {
        this.code = code;
        this.message = message;
    }

    public override string ToString() => $"[{code}]: {message}";
    
    // These errors can be in separate classes in the future.
    // These are only examples of how to create different errors

    // default http errors
    public static Error NotFound() => new Error("Not found!", 404);

    // User validation
    public static Error UserDoesNotExists(int userId) => new Error($"User with id {userId} does not exist!", 404);
    
    // Custom errors
    public static Error BadInput(string msg) => new Error(msg, 400);
}
