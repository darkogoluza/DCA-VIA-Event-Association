using IntegrationTests.Common;
using Microsoft.Extensions.DependencyInjection;
using ViaEventAssociation.Core.Application.Common.CommandDispatcher;
using ViaEventAssociation.Core.Application.Features.Event.CreateEvent;
using ViaEventAssociation.Core.Application.Features.Event.UpdateVisibility;
using VIAEventAssociation.Core.Domain.Common.Repositories;
using ViaEventAssociation.Infrastructure.SqliteDmPersistence;

namespace IntegrationTests.Repositories.VeaEvent;

public class UpdateVisibilityIntegrationTests : IDisposable
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly ServiceProvider _serviceProvider;

    public UpdateVisibilityIntegrationTests()
    {
        _serviceProvider = TestServiceProviderCommandHandler.CreateServiceProvider();

        _commandDispatcher = _serviceProvider.GetRequiredService<ICommandDispatcher>();
    }

    public void Dispose()
    {
        _serviceProvider.GetRequiredService<SqliteDmContext>().ChangeTracker.Clear();
    }

    [Fact]
    public async Task UpdateEventVisibilityToTrue_ShouldReturnSuccess_WhenEventIsDraft()
    {
        // Arrange
        var repo = _serviceProvider.GetRequiredService<IEventRepository>();
        var commandCreate = CreateEventCommand.Create().payload;
        await _commandDispatcher.DispatchAsync(commandCreate);

        var commandUpdateVisibility =
            UpdateEventVisibilityCommand.Create(commandCreate.VeaEvent.VeaEventId.Id, true).payload;

        // Act
        var result = await _commandDispatcher.DispatchAsync(commandUpdateVisibility);

        // Assert
        Assert.True(result.isSuccess);
        Assert.Equal(true, (await repo.GetAsync(commandCreate.VeaEvent.Id))._visibility);
    }

    [Fact]
    public async Task UpdateEventVisibilityToFalse_ShouldReturnSuccess_WhenEventIsDraft()
    {
        // Arrange
        var repo = _serviceProvider.GetRequiredService<IEventRepository>();
        var commandCreate = CreateEventCommand.Create().payload;
        await _commandDispatcher.DispatchAsync(commandCreate);

        var commandUpdateVisibility =
            UpdateEventVisibilityCommand.Create(commandCreate.VeaEvent.VeaEventId.Id, false).payload;

        // Act
        var result = await _commandDispatcher.DispatchAsync(commandUpdateVisibility);

        // Assert
        Assert.True(result.isSuccess);
        Assert.Equal(false, (await repo.GetAsync(commandCreate.VeaEvent.Id))._visibility);
    }
}
