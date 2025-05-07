using System.Globalization;
using Microsoft.EntityFrameworkCore;
using VIAEventAssociation.Core.Domain.Contracts;
using ViaEventAssociation.Core.QueryContracts.Contract;
using ViaEventAssociation.Core.QueryContracts.Queries;

namespace ViaEventAssociation.Infrastructure.EfcQueries.Queries;

public class PersonalProfilePageHandler(VeadatabaseProductionContext context, CurrentDateTime currentDateTime)
    : IQueryHandler<PersonalProfilePage.Query, PersonalProfilePage.Answer>
{
    public async Task<PersonalProfilePage.Answer> HandleAsync(PersonalProfilePage.Query query)
    {
        string formattedDateTime = currentDateTime().ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);

        PersonalProfilePage.Guest guest = await context.Guests
            .Where(g => g.Id == query.GuestId)
            .Select(g => new PersonalProfilePage.Guest(g.FirstName + " " + g.LastName, g.Email, g.ProfilePictureUrl))
            .SingleAsync();

        List<PersonalProfilePage.UpcomingEvent> upcomingEvents = await context.Events
            .OrderBy(e => e.StartDateTime)
            .Where(e => e.StartDateTime.CompareTo(formattedDateTime) > 0)
            .Select(e => new PersonalProfilePage.UpcomingEvent(e.Title, e.Guests.Count(),
                DateOnly.FromDateTime(DateTime.ParseExact("2024-04-30 15:00", "yyyy-MM-dd HH:mm",
                    CultureInfo.InvariantCulture)),
                TimeOnly.FromDateTime(DateTime.ParseExact("2024-04-30 15:00", "yyyy-MM-dd HH:mm",
                    CultureInfo.InvariantCulture))))
            .ToListAsync();


        List<PersonalProfilePage.PastEvent> pastEvents = await context.Events
            .OrderBy(e => e.StartDateTime)
            .Take(5)
            .Where(e => e.StartDateTime.CompareTo(formattedDateTime) < 0)
            .Select(e => new PersonalProfilePage.PastEvent(e.Title))
            .ToListAsync();

        int pendingInvitationsCount = await context.Invitations
            .Where(i => i.InviteeId == query.GuestId)
            .CountAsync();

        return new PersonalProfilePage.Answer(guest, upcomingEvents.Count(), upcomingEvents, pastEvents,
            pendingInvitationsCount);
    }
}
