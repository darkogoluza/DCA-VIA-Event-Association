using IntegrationTests.Common;
using Microsoft.Extensions.DependencyInjection;
using ViaEventAssociation.Core.Application.Common.CommandDispatcher;
using ViaEventAssociation.Core.Application.Features.Event.CreateEvent;
using ViaEventAssociation.Core.Application.Features.Event.UpdateMaxNoOfGuests;
using VIAEventAssociation.Core.Domain.Common.Repositories;
using ViaEventAssociation.Infrastructure.SqliteDmPersistence;

namespace IntegrationTests.Repositories.VeaEvent;

public class UpdateMaxNoOfGuestsIntegrationTests : IDisposable
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly ServiceProvider _serviceProvider;

    public UpdateMaxNoOfGuestsIntegrationTests()
    {
        _serviceProvider = TestServiceProvider.CreateServiceProvider();

        _commandDispatcher = _serviceProvider.GetRequiredService<ICommandDispatcher>();
    }

    public void Dispose()
    {
        _serviceProvider.GetRequiredService<SqliteDmContext>().ChangeTracker.Clear();
    }

    [Fact]
    public async Task UpdateEventMaxNoOfGuests_ShouldReturnSuccess_WhenEventIsDraft()
    {
        // Arrange
        var repo = _serviceProvider.GetRequiredService<IEventRepository>();
        var commandCreate = CreateEventCommand.Create().payload;
        await _commandDispatcher.DispatchAsync(commandCreate);

        var commandUpdateMaxNoOfGuests =
            UpdateEventMaxNoOfGuestsCommand.Create(commandCreate.VeaEvent.VeaEventId.Id, 10).payload;

        // Act
        var result = await _commandDispatcher.DispatchAsync(commandUpdateMaxNoOfGuests);

        // Assert
        Assert.True(result.isSuccess);
        Assert.Equal(10, (await repo.GetAsync(commandCreate.VeaEvent.Id))._maxNoOfGuests?.Value);
    }
}
