using IntegrationTests.Common;
using Microsoft.Extensions.DependencyInjection;
using ViaEventAssociation.Core.Application.Common.CommandDispatcher;
using ViaEventAssociation.Core.Application.Features.Event.CreateEvent;
using ViaEventAssociation.Core.Application.Features.Event.ReadieEvent;
using ViaEventAssociation.Core.Application.Features.Event.UpdateDescription;
using ViaEventAssociation.Core.Application.Features.Event.UpdateMaxNoOfGuests;
using ViaEventAssociation.Core.Application.Features.Event.UpdateStartAndEndDateTime;
using ViaEventAssociation.Core.Application.Features.Event.UpdateTitle;
using ViaEventAssociation.Core.Application.Features.Event.UpdateVisibility;
using VIAEventAssociation.Core.Domain.Common.Repositories;
using VIAEventAssociation.Core.Domain.Common.Values;
using ViaEventAssociation.Infrastructure.SqliteDmPersistence;

namespace IntegrationTests.Repositories.VeaEvent;

public class ReadieEventIntegrationTests : IDisposable
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly ServiceProvider _serviceProvider;

    public ReadieEventIntegrationTests()
    {
        _serviceProvider = TestServiceProvider.CreateServiceProvider();

        _commandDispatcher = _serviceProvider.GetRequiredService<ICommandDispatcher>();
    }

    public void Dispose()
    {
        _serviceProvider.GetRequiredService<SqliteDmContext>().ChangeTracker.Clear();
    }

    [Fact]
    public async Task ReadieEvent_ShouldReturnSuccess_WhenEventIsDraft()
    {
        // Arrange
        var repo = _serviceProvider.GetRequiredService<IEventRepository>();
        var commandCreate = CreateEventCommand.Create().payload;
        await _commandDispatcher.DispatchAsync(commandCreate);

        var commandUpdateTitle =
            UpdateEventTitleCommand.Create(commandCreate.VeaEvent.VeaEventId.Id, "New Title").payload;
        var commandUpdateDescription = UpdateEventDescriptionCommand
            .Create(commandCreate.VeaEvent.VeaEventId.Id, "New Description").payload;
        var commandUpdateMaxNoOfGuests =
            UpdateEventMaxNoOfGuestsCommand.Create(commandCreate.VeaEvent.VeaEventId.Id, 10).payload;
        var commandUpdateStartEndDateTime = UpdateEventStartAndEndDateTimeCommand
            .Create(commandCreate.VeaEvent.VeaEventId.Id, new DateTime(2025, 3, 4, 12, 0, 0),
                new DateTime(2025, 3, 4, 13, 0, 0)).payload;
        var commandUpdateVisibility =
            UpdateEventVisibilityCommand.Create(commandCreate.VeaEvent.VeaEventId.Id, true).payload;

        DateTime CurrentDateTimeMock() => new DateTime(2025, 3, 3, 12, 0, 0);
        var commandReadieEvent =
            ReadieEventCommand.Create(commandCreate.VeaEvent.VeaEventId.Id, CurrentDateTimeMock).payload;

        await _commandDispatcher.DispatchAsync(commandUpdateTitle);
        await _commandDispatcher.DispatchAsync(commandUpdateDescription);
        await _commandDispatcher.DispatchAsync(commandUpdateMaxNoOfGuests);
        await _commandDispatcher.DispatchAsync(commandUpdateStartEndDateTime);
        await _commandDispatcher.DispatchAsync(commandUpdateVisibility);

        // Act
        var result = await _commandDispatcher.DispatchAsync(commandReadieEvent);

        // Assert
        Assert.True(result.isSuccess);
        Assert.Equal(EventStatusType.Ready, (await repo.GetAsync(commandCreate.VeaEvent.Id))._eventStatusType);
    }
}
