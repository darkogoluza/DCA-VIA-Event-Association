using IntegrationTests.Common;
using Microsoft.Extensions.DependencyInjection;
using ViaEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Core.QueryContracts.QueryDispatching;

namespace IntegrationTests.CQRS;

public class BrowseUpcomingEventsIntegrationTest
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly ServiceProvider _serviceProvider;

    public BrowseUpcomingEventsIntegrationTest()
    {
        _serviceProvider = TestServiceProviderCQRS.CreateServiceProvider();

        _queryDispatcher = _serviceProvider.GetRequiredService<IQueryDispatcher>();
    }

    [Fact]
    public async Task Should_ReturnPaginatedUpcomingEvents_WhenPageAndPageSizeProvided()
    {
        // Arrange
        string searchText = "Art";
        int page = 1;
        int pageSize = 3;

        // Act
        var result = await _queryDispatcher.DispatchAsync(new BrowseUpcomingEvents.Query(searchText, page, pageSize));

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Events);
        Assert.True(result.Events.ToList().Count <= 6);
        Assert.True(result.MaxNumberOfPages >= 0);
    }

    [Fact]
    public async Task Should_ReturnFilteredEvents_WhenSearchTextIsProvided()
    {
        // Arrange
        string searchText = "Art";
        int page = 1;
        int pageSize = 3;

        // Act
        var result = await _queryDispatcher.DispatchAsync(new BrowseUpcomingEvents.Query(searchText, page, pageSize));

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Events);
        Assert.True(result.MaxNumberOfPages >= 1);
        Assert.True(result.Events.ToList().Count <= pageSize);
    }
}
