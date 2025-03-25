using UnitTests.Fakes;
using UnitTests.Stubs;
using ViaEventAssociation.Core.Application.Common.CommandHandler;
using ViaEventAssociation.Core.Application.Features.Event.CreateEvent;
using ViaEventAssociation.Core.Application.Features.Event.UpdateTitle;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Values;
using VIAEventAssociation.Core.Domain.Common.Repositories;

namespace UnitTests.Features.Event.UpdateTitle;

public class EventTitleCommandHandlerTest
{
    private readonly InMemEventRepoStub repo = new();
    private VeaEvent _veaEvent;

    public EventTitleCommandHandlerTest()
    {
        IUnitOfWork uow = new FakeUoW();
        ICommandHandler<CreateEventCommand> handler = new CreateEventHandler(repo, uow);

        CreateEventCommand command = CreateEventCommand.Create().payload;
        handler.HandleAsync(command);
        _veaEvent = repo.Events[0];
    }

    [Fact]
    public async Task UpdateTitle()
    {
        // Arrange
        IUnitOfWork uow = new FakeUoW();
        ICommandHandler<UpdateEventTitleCommand> handler = new UpdateEventTitleHandler(repo, uow);

        UpdateEventTitleCommand command = UpdateEventTitleCommand.Create(_veaEvent.VeaEventId.Id, "New Title").payload;

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        Assert.True(result.isSuccess);
        Assert.Single(repo.Events);
        Assert.Equal(command.VeaEventId.Id, _veaEvent.VeaEventId.Id);
    }
}
