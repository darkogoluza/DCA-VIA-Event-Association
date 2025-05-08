using ViaEventAssociation.Core.QueryContracts.Contract;

namespace ViaEventAssociation.Core.QueryContracts.Queries;

public class BrowseUpcomingEvents
{
    public record Query(string Search, int Page, int PageSize) : IQuery<Answer>;

    public record Answer(
        IEnumerable<Event> Events,
        int MaxNumberOfPages);

    public record Event(
        string StartDateTime,
        string EventTitle,
        string EventDescription,
        bool Visibility,
        GuestsCount GuestsCount
    );

    public record GuestsCount(int AttendeesCount, int AvailableSlotsCount);
}
