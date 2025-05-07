using ViaEventAssociation.Infrastructure.EfcQueries;

namespace UnitTests.CQRS;

public class ContextUnitTests
{
    [Fact]
    public async Task DbHasGuestsSeed()
    {
        await using var ctx = VeadatabaseProductionContext.SetupReadContext().Seed();
        Assert.NotEmpty(ctx.Guests);
        Assert.Equal(50, ctx.Guests.Count());
    }

    [Fact]
    public async Task DbHasEventsSeed()
    {
        await using var ctx = VeadatabaseProductionContext.SetupReadContext().Seed();
        Assert.NotEmpty(ctx.Events);
        Assert.Equal(28, ctx.Events.Count());
    }

    [Fact]
    public async Task DbHasInvitationsSeed()
    {
        await using var ctx = VeadatabaseProductionContext.SetupReadContext().Seed();
        Assert.NotEmpty(ctx.Invitations);
        Assert.Equal(149, ctx.Invitations.Count());
    }

    [Fact]
    public async Task DbHasJoinRequestsSeed()
    {
        await using var ctx = VeadatabaseProductionContext.SetupReadContext().Seed();
        Assert.NotEmpty(ctx.RequestToJoins);
        Assert.Equal(94, ctx.RequestToJoins.Count());
    }
}
