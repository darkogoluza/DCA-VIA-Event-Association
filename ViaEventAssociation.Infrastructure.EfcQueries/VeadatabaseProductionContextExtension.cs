using ViaEventAssociation.Infrastructure.EfcQueries.SeedFactories;

namespace ViaEventAssociation.Infrastructure.EfcQueries;

public static class VeadatabaseProductionContextExtensions
{
    public static VeadatabaseProductionContext Seed(this VeadatabaseProductionContext context)
    {
        context.Guests.AddRange(GuestSeedFactory.CreateGuests());
        context.Events.AddRange(EventSeedFactory.CreateEvents());
        context.SaveChanges();

        InvitationSeedFactory.Seed(context);
        context.SaveChanges();

        JoinRequestSeedFactory.Seed(context);
        context.SaveChanges();

        ParticipationSeedFactory.AddParticipationsToEvent(context);
        context.SaveChanges();

        return context;
    }
}
