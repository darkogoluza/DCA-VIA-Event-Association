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
    public override bool Equals(object obj)
    {
        if (obj is not Error other) return false;
        return code == other.code && message == other.message;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(code, message);
    }

    public static bool operator ==(Error left, Error right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Error left, Error right)
    {
        return !Equals(left, right);
    }
    
    // These errors can be in separate classes in the future.
    // These are only examples of how to create different errors

    // default http errors
    public static Error NotFound() => new Error("Not found!", 404);

    // User validation
    public static Error UserDoesNotExists(int userId) => new Error($"User with id {userId} does not exist!", 404);
    
    // Custom errors
    public static Error BadInput(string msg) => new Error(msg, 400);
    public static Error BadTitle() => BadInput("Title length is between 3 and 75 (inclusive) characters.");
    public static Error CanNotModifyActiveEvent() => new Error( "Can not modify an active event.", 403 );
    public static Error CanNotModifyCancelledEvent() => new Error( "Can not modify a cancelled event.", 403 );
}
