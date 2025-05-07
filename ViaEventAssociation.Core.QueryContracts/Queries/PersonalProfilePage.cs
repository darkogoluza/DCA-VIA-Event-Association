using ViaEventAssociation.Core.QueryContracts.Contract;

namespace ViaEventAssociation.Core.QueryContracts.Queries;

public class PersonalProfilePage
{
    public record Query(string GuestId) : IQuery<Answer>;

    public record Answer(
        Guest Guest,
        int UpcomingEventsCount,
        IEnumerable<UpcomingEvent> UpcomingEvents,
        IEnumerable<PastEvent> PastEvents,
        int PendingInvitationsCount);

    public record Guest(string Name, string Email, string ProfilePictureUrl);

    public record UpcomingEvent(string EventTitle, int NumberOfAttendees, DateOnly Date, TimeOnly StartTime);

    public record PastEvent(string EventTitle);
}
