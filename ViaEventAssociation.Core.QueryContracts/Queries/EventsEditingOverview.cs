using ViaEventAssociation.Core.QueryContracts.Contract;

namespace ViaEventAssociation.Core.QueryContracts.Queries;

public class EventsEditingOverview
{
    public record Query() : IQuery<Answer>;

    public record Answer(
        IEnumerable<Event> DraftEvents,
        IEnumerable<Event> ReadyEvents,
        IEnumerable<Event> CancelledEvents
    );

    public record Event(string EventTitle);
}
