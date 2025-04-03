using UnitTests.Fakes;
using UnitTests.Stubs;
using ViaEventAssociation.Core.Application.Common.CommandHandler;
using ViaEventAssociation.Core.Application.Features.Event.CreateEvent;
using ViaEventAssociation.Core.Application.Features.Event.UpdateDescription;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;
using VIAEventAssociation.Core.Domain.Common.Repositories;

namespace UnitTests.Features.Event.UpdateDescription;

public class EventDescriptionCommandHandlerTest
{
    private readonly InMemEventRepoStub repo = new();
    private VeaEvent _veaEvent;

    public EventDescriptionCommandHandlerTest()
    {
        ICommandHandler<CreateEventCommand> handler = new CreateEventHandler(repo);

        CreateEventCommand command = CreateEventCommand.Create().payload;
        handler.HandleAsync(command);
        _veaEvent = repo.Events[0];
    }

    [Fact]
    public async Task UpdateDescription()
    {
        // Arrange
        IUnitOfWork uow = new FakeUoW();
        ICommandHandler<UpdateEventDescriptionCommand> handler = new UpdateEventDescriptionHandler(repo);

        UpdateEventDescriptionCommand command = UpdateEventDescriptionCommand.Create(_veaEvent.VeaEventId.Id, "New description").payload;

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        Assert.True(result.isSuccess);
        Assert.Single(repo.Events);
        Assert.Equal(command.VeaEventId.Id, _veaEvent.VeaEventId.Id);
    }
}
