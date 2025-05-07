using IntegrationTests.Common;
using Microsoft.Extensions.DependencyInjection;
using ViaEventAssociation.Core.Application.Common.CommandDispatcher;
using ViaEventAssociation.Core.Application.Features.Event.CreateEvent;
using VIAEventAssociation.Core.Domain.Common.Repositories;
using ViaEventAssociation.Infrastructure.SqliteDmPersistence;

namespace IntegrationTests.Repositories.VeaEvent;

public class CreateVeaEventIntegrationTests : IDisposable
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly ServiceProvider _serviceProvider;

    public CreateVeaEventIntegrationTests()
    {
        _serviceProvider = TestServiceProviderCommandHandler.CreateServiceProvider();

        _commandDispatcher = _serviceProvider.GetRequiredService<ICommandDispatcher>();
    }

    public void Dispose()
    {
        _serviceProvider.GetRequiredService<SqliteDmContext>().ChangeTracker.Clear();
    }

    [Fact]
    public async Task CreateEventCommand_ShouldSucceedAndPersistEvent()
    {
        // Arrange
        var repo = _serviceProvider.GetRequiredService<IEventRepository>();
        var command = CreateEventCommand.Create().payload;

        // Act
        var result = await _commandDispatcher.DispatchAsync(command);

        // Assert
        Assert.True(result.isSuccess);
        Assert.Single(await repo.GetAllAsync());
        Assert.Equal(command.VeaEvent, await repo.GetAsync(command.VeaEvent.Id));
    }
}
