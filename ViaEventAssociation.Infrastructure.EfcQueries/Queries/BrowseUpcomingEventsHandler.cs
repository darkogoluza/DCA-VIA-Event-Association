using Microsoft.EntityFrameworkCore;
using ViaEventAssociation.Core.QueryContracts.Contract;
using ViaEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Infrastructure.EfcQueries.Models;

namespace ViaEventAssociation.Infrastructure.EfcQueries.Queries;

public class BrowseUpcomingEventsHandler(VeadatabaseProductionContext context)
    : IQueryHandler<BrowseUpcomingEvents.Query, BrowseUpcomingEvents.Answer>
{
    public async Task<BrowseUpcomingEvents.Answer> HandleAsync(BrowseUpcomingEvents.Query query)
    {
        IQueryable<Event> baseQuery = context.Events;

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            baseQuery = baseQuery.Where(e => e.Title.Contains(query.Search));
        }

        List<BrowseUpcomingEvents.Event> events = await baseQuery
            .OrderByDescending(e => e.StartDateTime)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(e => new BrowseUpcomingEvents.Event(
                e.StartDateTime,
                e.Title,
                e.Description,
                e.Visibility == 1,
                new BrowseUpcomingEvents.GuestsCount(
                    e.Guests.Count,
                    e.MaxNoOfGuests ?? -1)))
            .ToListAsync();

        int totalEventCount = await baseQuery.CountAsync();
        int maxNumberOfPages = (int)Math.Ceiling((double)totalEventCount / query.PageSize);

        return new BrowseUpcomingEvents.Answer(events, maxNumberOfPages);
    }
}
