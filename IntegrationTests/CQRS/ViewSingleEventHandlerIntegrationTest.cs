using IntegrationTests.Common;
using Microsoft.Extensions.DependencyInjection;
using ViaEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Core.QueryContracts.QueryDispatching;

namespace IntegrationTests.CQRS;

public class ViewSingleEventHandlerIntegrationTest
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly ServiceProvider _serviceProvider;

    public ViewSingleEventHandlerIntegrationTest()
    {
        _serviceProvider = TestServiceProviderCQRS.CreateServiceProvider();

        _queryDispatcher = _serviceProvider.GetRequiredService<IQueryDispatcher>();
    }

    [Fact]
    public async Task ViewSingleEvent_ReturnsValidEventDataAndPaginatedGuests()
    {
        // Arrange 
        var eventId = "27a5bde5-3900-4c45-9358-3d186ad6b2d7"; // Use a known existing test event ID
        int offset = 1;
        int limit = 5;

        // Act
        var result = await _queryDispatcher.DispatchAsync(
            new ViewSingleEvent.Query(eventId, offset, limit));

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Event);
        Assert.False(string.IsNullOrWhiteSpace(result.Event.Title));
        Assert.False(string.IsNullOrWhiteSpace(result.Event.Description));
        Assert.True(DateTime.Parse(result.Event.StartDateTime) < DateTime.Parse(result.Event.EndDateTime));

        Assert.True(result.GuestsCount.AttendeesCount >= 0);
        Assert.True(result.GuestsCount.AvailableSlotsCount >= 0);

        var guests = result.GuestList.Guests;
        Assert.NotNull(guests);
        Assert.InRange(guests.ToList().Count, 0, limit);
    }
}
