using Microsoft.EntityFrameworkCore;
using ViaEventAssociation.Core.QueryContracts.Contract;
using ViaEventAssociation.Core.QueryContracts.Queries;

namespace ViaEventAssociation.Infrastructure.EfcQueries.Queries;

public class EventsEditingOverviewHandler(VeadatabaseProductionContext context)
    : IQueryHandler<EventsEditingOverview.Query, EventsEditingOverview.Answer>
{
    public async Task<EventsEditingOverview.Answer> HandleAsync(EventsEditingOverview.Query query)
    {
        IEnumerable<EventsEditingOverview.Event> draftEvents = await context.Events
            .Where(e => e.EventStatusType == "draft")
            .Select(e => new EventsEditingOverview.Event(e.Title))
            .ToListAsync();

        IEnumerable<EventsEditingOverview.Event> readyEvents = await context.Events
            .Where(e => e.EventStatusType == "ready")
            .Select(e => new EventsEditingOverview.Event(e.Title))
            .ToListAsync();

        IEnumerable<EventsEditingOverview.Event> cancelledEvent = await context.Events
            .Where(e => e.EventStatusType == "cancelled")
            .Select(e => new EventsEditingOverview.Event(e.Title))
            .ToListAsync();

        return new EventsEditingOverview.Answer(draftEvents, readyEvents, cancelledEvent);
    }
}
