﻿namespace ViaEventAssociation.Core.Tools.OperationResult;

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
    public static Error Default() => new Error("Something went wrong!", 500);

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

    public static Error EventDurationTooShort() =>
        BadInput("The event duration is too short, expected duration between 1h and 10h.");

    public static Error EventDurationTooLong() =>
        BadInput("The event duration is too long, expected duration between 1h and 10h.");

    public static Error TitleNotSet() => BadInput("The title is empty.");
    public static Error DescriptionNotSet() => BadInput("The description is empty.");
    public static Error TimesAreNotSet() => BadInput("The start and end date time is empty.");
    public static Error MaximumNumberOfGuestsIsNotSet() => BadInput("The maximum number of guests is not defined.");
    public static Error VisibilityIsNotSet() => BadInput("The visibility has not been set.");
    public static Error EventIsInPast() => BadInput("Event is in the past.");

    public static Error CanNotActivateEventThatIsNotReady() =>
        new Error("Can not activate an event that has not been readied.", 403);

    public static Error CanNotJoinDraftEvent() => new Error("Guest can not join an event that is in draft state.", 403);

    public static Error CanNotJoinCancelledEvent() =>
        new Error("Guest can not join an event that is in draft state.", 403);

    public static Error CanNotJoinEventIsFull() => new Error("Guest can not join an event that is full.", 403);
    public static Error GuestIsAlreadyParticipating() => new Error("Guest can not join the event twice.", 403);
    public static Error CanNotJoinPrivateEvent() => new Error("Guest can not join an private event.", 403);
    public static Error CanNotInviteToDraftEvent() => new Error("Can not invite a guest to a draft event", 403);
    public static Error CanNotInviteToCancelledEvent() => new Error("Can not invite a guest to a cancelled event", 403);
    public static Error CanNotInviteEventIsFull() => new Error("Can not invite a guest to an event that is full.", 403);
    public static Error GuestAlreadyInvited() => new Error("Can not invite a guest twice.", 403);

    // Start and end DateTime
    public static Error StartDateTimeIsBiggerThenEndDateTime() =>
        BadInput("The end date/time can not be bigger then start date/time.");

    public static Error StartTimeTooEarly() => BadInput("The start time is too early, it must not be before 08:00");

    public static Error EndTimeTooLate() =>
        BadInput("The end time is too late, closing hours are between 01:00 and 08:00");

    public static Error InvalidTimeSpan() =>
        BadInput("The time spans between 01:00 and 08:00, which are closing hours.");

    public static Error StartTimeInPast() => new Error("The start time can not be in the past.", 403);

    // Max number of guests
    public static Error CanNotReduceMaxNoOfGuestsOnActiveEvent() =>
        new Error("Can not set lower amount of guests on an active event", 403);

    public static Error MaxNoOfGuestsTooSmall() =>
        BadInput("Maximum number of guests is too small, must be higher then 5");

    public static Error MaxNoOfGuestsTooLarge() =>
        BadInput("Maximum number of guests is too large, must be lower then 50");

    // Email
    public static Error WrongEmailDomain() => BadInput("Only people with a VIA mail can register.");

    public static Error WrongEmailFormat() => BadInput("The emails is of a wrong format.");

    // First Name
    public static Error WrongFirstNameFormat() => BadInput("First name is ofa wrong format.");

    // Last Name
    public static Error WrongLastNameFormat() => BadInput("Last name is ofa wrong format.");

    // Invitation
    public static Error InvitationNotFound() => NotFound();

    public static Error CanNotAcceptInvitationOnCancelledEvent() =>
        new Error("Can not accept an invitation on a cancelled event.", 403);

    public static Error CanNotAcceptInvitationOnReadiedEvent() =>
        new Error("Can not accept an invitation on a readied event.", 403);

    public static Error CanNotAcceptInvitationEventIsFull() =>
        new Error("Can not accept an invitation the event is full.", 403);

    public static Error CanNotDeclineInvitationOnCancelledEvent() =>
        new Error("Can not decline an invitation on a cancelled event.", 403);
}
