using IntegrationTests.Common;
using Microsoft.Extensions.DependencyInjection;
using ViaEventAssociation.Core.Application.Common.CommandDispatcher;
using ViaEventAssociation.Core.Application.Features.Event.CreateEvent;
using ViaEventAssociation.Core.Application.Features.Event.UpdateTitle;
using VIAEventAssociation.Core.Domain.Common.Repositories;
using ViaEventAssociation.Infrastructure.SqliteDmPersistence;

namespace IntegrationTests.Repositories.VeaEvent;

public class UpdateTitleIntegrationTests : IDisposable
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly ServiceProvider _serviceProvider;

    public UpdateTitleIntegrationTests()
    {
        _serviceProvider = TestServiceProvider.CreateServiceProvider();

        _commandDispatcher = _serviceProvider.GetRequiredService<ICommandDispatcher>();
    }

    public void Dispose()
    {
        _serviceProvider.GetRequiredService<SqliteDmContext>().ChangeTracker.Clear();
    }

    [Fact]
    public async Task UpdateEventTitle_ShouldReturnSuccess_WhenEventIsDraft()
    {
        // Arrange
        var repo = _serviceProvider.GetRequiredService<IEventRepository>();
        var commandCreate = CreateEventCommand.Create().payload;
        await _commandDispatcher.DispatchAsync(commandCreate);

        var commandUpdateTitle =
            UpdateEventTitleCommand.Create(commandCreate.VeaEvent.VeaEventId.Id, "New title").payload;

        // Act
        var result = await _commandDispatcher.DispatchAsync(commandUpdateTitle);

        // Assert
        Assert.True(result.isSuccess);
        Assert.Equal("New title", (await repo.GetAsync(commandCreate.VeaEvent.Id))._title?.Value);
    }
}
