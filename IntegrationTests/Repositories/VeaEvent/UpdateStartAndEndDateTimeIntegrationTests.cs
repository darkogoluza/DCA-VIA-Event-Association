using IntegrationTests.Common;
using Microsoft.Extensions.DependencyInjection;
using ViaEventAssociation.Core.Application.Common.CommandDispatcher;
using ViaEventAssociation.Core.Application.Features.Event.CreateEvent;
using ViaEventAssociation.Core.Application.Features.Event.UpdateStartAndEndDateTime;
using VIAEventAssociation.Core.Domain.Common.Repositories;
using ViaEventAssociation.Infrastructure.SqliteDmPersistence;

namespace IntegrationTests.Repositories.VeaEvent;

public class UpdateStartAndEndDateTimeIntegrationTests : IDisposable
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly ServiceProvider _serviceProvider;

    public UpdateStartAndEndDateTimeIntegrationTests()
    {
        _serviceProvider = TestServiceProviderCommandHandler.CreateServiceProvider();

        _commandDispatcher = _serviceProvider.GetRequiredService<ICommandDispatcher>();
    }

    public void Dispose()
    {
        _serviceProvider.GetRequiredService<SqliteDmContext>().ChangeTracker.Clear();
    }

    [Fact]
    public async Task UpdateEventStartAndEndDateTime_ShouldReturnSuccess_WhenEventIsDraft()
    {
        // Arrange
        var repo = _serviceProvider.GetRequiredService<IEventRepository>();
        var commandCreate = CreateEventCommand.Create().payload;
        await _commandDispatcher.DispatchAsync(commandCreate);

        DateTime start = new DateTime(2023, 3, 4, 12, 0, 0);
        DateTime end = new DateTime(2023, 3, 4, 14, 0, 0);

        var commandUpdateStartAndEndDateTime =
            UpdateEventStartAndEndDateTimeCommand.Create(commandCreate.VeaEvent.VeaEventId.Id, start, end).payload;

        // Act
        var result = await _commandDispatcher.DispatchAsync(commandUpdateStartAndEndDateTime);

        // Assert
        Assert.True(result.isSuccess);
        Assert.Equal(start, (await repo.GetAsync(commandCreate.VeaEvent.Id))._startDateTime);
        Assert.Equal(end, (await repo.GetAsync(commandCreate.VeaEvent.Id))._endDateTime);
    }
}
