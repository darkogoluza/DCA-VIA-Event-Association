using IntegrationTests.Common;
using Microsoft.Extensions.DependencyInjection;
using ViaEventAssociation.Core.Application.Common.CommandDispatcher;
using ViaEventAssociation.Core.Application.Features.Event.CreateEvent;
using ViaEventAssociation.Core.Application.Features.Event.UpdateDescription;
using VIAEventAssociation.Core.Domain.Common.Repositories;
using ViaEventAssociation.Infrastructure.SqliteDmPersistence;

namespace IntegrationTests.Repositories.VeaEvent;

public class UpdateDescriptionIntegrationTests : IDisposable
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly ServiceProvider _serviceProvider;

    public UpdateDescriptionIntegrationTests()
    {
        _serviceProvider = TestServiceProviderCommandHandler.CreateServiceProvider();

        _commandDispatcher = _serviceProvider.GetRequiredService<ICommandDispatcher>();
    }

    public void Dispose()
    {
        _serviceProvider.GetRequiredService<SqliteDmContext>().ChangeTracker.Clear();
    }

    [Fact]
    public async Task UpdateEventDescription_ShouldReturnSuccess_WhenEventIsDraft()
    {
        // Arrange
        var repo = _serviceProvider.GetRequiredService<IEventRepository>();
        var commandCreate = CreateEventCommand.Create().payload;
        await _commandDispatcher.DispatchAsync(commandCreate);

        var commandUpdateDescription = UpdateEventDescriptionCommand.Create(commandCreate.VeaEvent.VeaEventId.Id, "New description").payload;

        // Act
        var result = await _commandDispatcher.DispatchAsync(commandUpdateDescription);

        // Assert
        Assert.True(result.isSuccess);
        Assert.Equal("New description", (await repo.GetAsync(commandCreate.VeaEvent.Id))._description?.Value);
    }
}
