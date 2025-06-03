using IntegrationTests.Common;
using Microsoft.Extensions.DependencyInjection;
using ViaEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Core.QueryContracts.QueryDispatching;

namespace IntegrationTests.CQRS;

public class EventsEditingOverviewIntegrationTest
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly ServiceProvider _serviceProvider;

    public EventsEditingOverviewIntegrationTest()
    {
        _serviceProvider = TestServiceProviderCQRS.CreateServiceProvider();

        _queryDispatcher = _serviceProvider.GetRequiredService<IQueryDispatcher>();
    }

    [Fact]
    public async Task EventsEditingOverviewQuery_ShouldReturn_NonNullEventLists()
    {
        // Act
        var result = await _queryDispatcher.DispatchAsync(new EventsEditingOverview.Query());

        // Assert
        Assert.NotNull(result);

        Assert.NotNull(result.DraftEvents);
        Assert.IsAssignableFrom<IEnumerable<EventsEditingOverview.Event>>(result.DraftEvents);

        Assert.NotNull(result.ReadyEvents);
        Assert.IsAssignableFrom<IEnumerable<EventsEditingOverview.Event>>(result.ReadyEvents);

        Assert.NotNull(result.CancelledEvents);
        Assert.IsAssignableFrom<IEnumerable<EventsEditingOverview.Event>>(result.CancelledEvents);
    }
}
