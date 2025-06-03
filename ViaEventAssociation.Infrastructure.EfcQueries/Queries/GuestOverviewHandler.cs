using Microsoft.EntityFrameworkCore;
using ViaEventAssociation.Core.QueryContracts.Contract;
using ViaEventAssociation.Core.QueryContracts.Queries;

namespace ViaEventAssociation.Infrastructure.EfcQueries.Queries;

public class GuestOverviewHandler(VeadatabaseProductionContext context)
    : IQueryHandler<GuestOverview.Query, GuestOverview.Answer>
{
    public async Task<GuestOverview.Answer> HandleAsync(GuestOverview.Query query)
    {
        GuestOverview.Guest guest = await context.Guests
            .Where(g => g.Id == query.GuestId)
            .Select(g => new GuestOverview.Guest(g.FirstName + " " + g.LastName, g.ProfilePictureUrl))
            .SingleAsync();

        IEnumerable<GuestOverview.Participation> participations = await context.Guests
            .Where(g => g.Id == query.GuestId)
            .Select(g => g.Event!)
            .Where(e => e != null)
            .Select(e => new GuestOverview.Participation(e.Title))
            .ToListAsync();

        IEnumerable<GuestOverview.Invitation> invitations = await context.Invitations
            .Where(inv => inv.InviteeId == query.GuestId)
            .Include(inv => inv.Event)
            .Select(i => new GuestOverview.Invitation(i.Event.Title))
            .ToListAsync();

        IEnumerable<GuestOverview.JoinRequest> joinRequests = await context.RequestToJoins
            .Where(req => req.InvitorId == query.GuestId)
            .Include(req => req.VeaEvent)
            .Select(req => new GuestOverview.JoinRequest(req.StatusType, req.VeaEvent.Title))
            .ToListAsync();

        return new GuestOverview.Answer(guest, participations, invitations, joinRequests);
    }
}
