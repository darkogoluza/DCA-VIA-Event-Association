using UnitTests.Fakes;
using UnitTests.Stubs;
using ViaEventAssociation.Core.Application.Common.CommandHandler;
using ViaEventAssociation.Core.Application.Features.Event.UpdateTitle;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Values;
using VIAEventAssociation.Core.Domain.Common.Repositories;

namespace UnitTests.Features.Event.UpdateTitle;

public class EventTitleCommandHandlerTest
{
    private readonly InMemEventRepoStub repo = new();
    // private readonly Guid id = Guid.NewGuid(); For later

    public EventTitleCommandHandlerTest()
    {
        // Arrange
        var expectedTitleResult = Title.Create("Working Title");
        var expectedDescriptionResult = Description.Create("Some description");
        var expectedMaxNoOfGuestsResult = MaxNoOfGuests.Create(5);

        // Act
        VeaEvent veaEvent;
        veaEvent = VeaEvent.Create().payload;
        veaEvent._title = expectedTitleResult.payload;
        veaEvent._description = expectedDescriptionResult.payload;
        veaEvent._startDateTime = new DateTime(2025, 3, 4, 12, 0, 0);
        veaEvent._endDateTime = new DateTime(2025, 3, 4, 13, 0, 0);
        veaEvent._visibility = false;
        veaEvent._maxNoOfGuests = expectedMaxNoOfGuestsResult.payload;

        repo.AddAsync(veaEvent);
    }

    [Fact]
    public async Task UpdateTitle()
    {
        // Arrange
        IUnitOfWork uow = new FakeUoW();
        ICommandHandler<UpdateEventTitleCommand> handler = new UpdateEventTitleHandler(repo, uow);

        UpdateEventTitleCommand command = UpdateEventTitleCommand.Create(Guid.NewGuid(), "New Title").payload;

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        Assert.True(result.isSuccess);
        Assert.Single(repo.Events);

        VeaEvent veaEvent = repo.Events.First();
        // Assert.Equal(command.VeaEventId, veaEvent.VeaEventId); For later
    }
}
