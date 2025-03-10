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
    // Title
    public static Error BadTitle() => BadInput("Title length is between 3 and 75 (inclusive) characters.");
    // Description
    public static Error BadDescription() => BadInput("Description length is more then 250 characters.");
    // Event
    public static Error CanNotModifyActiveEvent() => new Error("Can not modify an active event.", 403);
    public static Error CanNotModifyCancelledEvent() => new Error("Can not modify a cancelled event.", 403);
    public static Error EventDurationTooShort() => BadInput("The event duration is too short, expected duration between 1h and 10h.");
    public static Error EventDurationTooLong() => BadInput("The event duration is too long, expected duration between 1h and 10h.");
    // Start and end DateTime
    public static Error StartDateTimeIsBiggerThenEndDateTime() => BadInput("The end date/time can not be bigger then start date/time.");
    public static Error StartTimeTooEarly() => BadInput("The start time is too early, it must not be before 08:00");
    public static Error EndTimeTooLate() => BadInput("The end time is too late, closing hours are between 01:00 and 08:00");
    public static Error InvalidTimeSpan() => BadInput("The time spans between 01:00 and 08:00, which are closing hours.");
    public static Error StartTimeInPast() => new Error("The start time can not be in the past.", 403);
}
