using UnitTests.Fakes;
using UnitTests.Stubs;
using ViaEventAssociation.Core.Application.Common.CommandHandler;
using ViaEventAssociation.Core.Application.Features.Event.CreateEvent;
using ViaEventAssociation.Core.Application.Features.Event.UpdateStartAndEndDateTime;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;
using VIAEventAssociation.Core.Domain.Common.Repositories;

namespace UnitTests.Features.Event.UpdateStartAndDateEndTime;

public class EventStartEndDateTimeCommandHandlerTest
{
    private readonly InMemEventRepoStub repo = new();
    private VeaEvent _veaEvent;

    public EventStartEndDateTimeCommandHandlerTest()
    {
        ICommandHandler<CreateEventCommand> handler = new CreateEventHandler(repo);

        CreateEventCommand command = CreateEventCommand.Create().payload;
        handler.HandleAsync(command);
        _veaEvent = repo.Events[0];
    }

    [Fact]
    public async Task UpdateStartAndEndDateTime()
    {
        // Arrange
        ICommandHandler<UpdateEventStartAndEndDateTimeCommand> handler = new UpdateEventStartAndEndDateTimeHandler(repo);
        DateTime start = new DateTime(2025, 03, 25, 13, 0, 0);
        DateTime end = new DateTime(2025, 03, 25, 16, 0, 0);

        UpdateEventStartAndEndDateTimeCommand command = UpdateEventStartAndEndDateTimeCommand.Create(_veaEvent.VeaEventId.Id, start, end).payload;

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        Assert.True(result.isSuccess);
        Assert.Single(repo.Events);
        Assert.Equal(command.VeaEventId.Id, _veaEvent.VeaEventId.Id);
    }
}
