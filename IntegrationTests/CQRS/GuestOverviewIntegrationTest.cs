using IntegrationTests.Common;
using Microsoft.Extensions.DependencyInjection;
using ViaEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Core.QueryContracts.QueryDispatching;

namespace IntegrationTests.CQRS;

public class GuestOverviewIntegrationTest
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly ServiceProvider _serviceProvider;

    public GuestOverviewIntegrationTest()
    {
        _serviceProvider = TestServiceProviderCQRS.CreateServiceProvider();

        _queryDispatcher = _serviceProvider.GetRequiredService<IQueryDispatcher>();
    }

    [Fact]
    public async Task Dispatching_GuestOverviewQuery_ReturnsInitializedGuestAndLists()
    {
        // Arrange
        var guestId = "230c1a99-d5c7-4fbc-9f48-07ccbb100936";

        // Act
        var result = await _queryDispatcher.DispatchAsync(new GuestOverview.Query(guestId));

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Guest);
        Assert.False(string.IsNullOrWhiteSpace(result.Guest.Name));
        Assert.False(string.IsNullOrWhiteSpace(result.Guest.ProfilePictureUrl));

        Assert.NotNull(result.Participations);
        Assert.IsAssignableFrom<IEnumerable<GuestOverview.Participation>>(result.Participations);

        Assert.NotNull(result.Invitations);
        Assert.IsAssignableFrom<IEnumerable<GuestOverview.Invitation>>(result.Invitations);

        Assert.NotNull(result.JoinRequests);
        Assert.IsAssignableFrom<IEnumerable<GuestOverview.JoinRequest>>(result.JoinRequests);
    }
}
