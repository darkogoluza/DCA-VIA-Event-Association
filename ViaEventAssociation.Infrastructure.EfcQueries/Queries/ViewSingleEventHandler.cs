using Microsoft.EntityFrameworkCore;
using ViaEventAssociation.Core.QueryContracts.Contract;
using ViaEventAssociation.Core.QueryContracts.Queries;

namespace ViaEventAssociation.Infrastructure.EfcQueries.Queries;

public class ViewSingleEventHandler(VeadatabaseProductionContext context)
    : IQueryHandler<ViewSingleEvent.Query, ViewSingleEvent.Answer>
{
    public async Task<ViewSingleEvent.Answer> HandleAsync(ViewSingleEvent.Query query)
    {
        var eventEntity = await context.Events
            .Include(e => e.Guests)
            .Include(e => e.Invitations)
            .ThenInclude(i => i.Invitee).Include(@event => @event.Location)
            .FirstOrDefaultAsync(e => e.Id == query.EventId);

        if (eventEntity == null)
            throw new Exception("Event not found");

        ViewSingleEvent.Event veaEvent = new(
            eventEntity.Title,
            eventEntity.Description,
            eventEntity.Location?.LocationId,
            eventEntity.StartDateTime,
            eventEntity.EndDateTime,
            eventEntity.Visibility == 1
        );

        var guestsQuery = eventEntity.Guests
            .Union(
                eventEntity.Invitations
                    .Where(inv => inv.StatusType == "Accepted" && inv.Invitee != null)
                    .Select(inv => inv.Invitee!)
            )
            .Distinct()
            .Select(g => new ViewSingleEvent.Guest(g.ProfilePictureUrl, g.FirstName + " " + g.LastName));

        var guests = guestsQuery
            .Skip(query.Offset)
            .Take(query.Limit)
            .ToList();

        var guestsCount = new ViewSingleEvent.GuestsCount(guestsQuery.Count(), eventEntity.MaxNoOfGuests ?? -1);
        var guestList = new ViewSingleEvent.GuestList(guests);

        return new ViewSingleEvent.Answer(veaEvent, guestsCount, guestList);
    }
}
