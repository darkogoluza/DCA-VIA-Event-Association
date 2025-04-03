using UnitTests.Fakes;
using UnitTests.Stubs;
using ViaEventAssociation.Core.Application.Common.CommandHandler;
using ViaEventAssociation.Core.Application.Features.Event.ActivateEvent;
using ViaEventAssociation.Core.Application.Features.Event.CreateEvent;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Values;
using VIAEventAssociation.Core.Domain.Common.Repositories;

namespace UnitTests.Features.Event.ActivateEvent;

public class ActivateEventCommandHandlerTest
{
    private readonly InMemEventRepoStub repo = new();
    private VeaEvent _veaEvent;
    private DateTime CurrentDateTimeMock() => new DateTime(2025, 3, 3, 12, 0, 0);

    public ActivateEventCommandHandlerTest()
    {
        ICommandHandler<CreateEventCommand> handler = new CreateEventHandler(repo);

        CreateEventCommand command = CreateEventCommand.Create().payload;
        handler.HandleAsync(command);
        _veaEvent = repo.Events[0];

        // Arrange
        var expectedTitleResult = Title.Create("Working Title");
        var expectedDescriptionResult = Description.Create("Some description");
        var expectedMaxNoOfGuestsResult = MaxNoOfGuests.Create(5);

        _veaEvent.UpdateTitle(expectedTitleResult.payload);
        _veaEvent.UpdateDescription(expectedDescriptionResult.payload);
        _veaEvent.SetMaxNoOfGuests(expectedMaxNoOfGuestsResult.payload);
        _veaEvent.UpdateStarEndDateTime(new DateTime(2025, 3, 4, 12, 0, 0), new DateTime(2025, 3, 4, 13, 0, 0));
        _veaEvent.SetVisibility(false);
        _veaEvent.Readie(CurrentDateTimeMock);
    }

    [Fact]
    public async Task ReadieHandler()
    {
        // Arrange
        ICommandHandler<ActivateEventCommand> handler = new ActivateEventHandler(repo);

        ActivateEventCommand command = ActivateEventCommand.Create(_veaEvent.VeaEventId.Id).payload;

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        Assert.True(result.isSuccess);
        Assert.Single(repo.Events);
        Assert.Equal(command.VeaEventId.Id, _veaEvent.VeaEventId.Id);
    }
}
