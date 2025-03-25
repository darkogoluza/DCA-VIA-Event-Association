using UnitTests.Fakes;
using UnitTests.Stubs;
using ViaEventAssociation.Core.Application.Common.CommandHandler;
using ViaEventAssociation.Core.Application.Features.Event.CreateEvent;
using ViaEventAssociation.Core.Application.Features.Event.UpdateVisibility;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;
using VIAEventAssociation.Core.Domain.Common.Repositories;

namespace UnitTests.Features.Event.UpdateVisibility;

public class EventVisibilityCommandHandlerTest
{
    private readonly InMemEventRepoStub repo = new();
    private VeaEvent _veaEvent;

    public EventVisibilityCommandHandlerTest()
    {
        IUnitOfWork uow = new FakeUoW();
        ICommandHandler<CreateEventCommand> handler = new CreateEventHandler(repo, uow);

        CreateEventCommand command = CreateEventCommand.Create().payload;
        handler.HandleAsync(command);
        _veaEvent = repo.Events[0];
    }

    [Fact]
    public async Task UpdateVisibility_Public()
    {
        // Arrange
        IUnitOfWork uow = new FakeUoW();
        ICommandHandler<UpdateEventVisibilityCommand> handler = new UpdateEventVisibilityHandler(repo, uow);

        UpdateEventVisibilityCommand command =
            UpdateEventVisibilityCommand.Create(_veaEvent.VeaEventId.Id, true).payload;

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        Assert.True(result.isSuccess);
        Assert.Single(repo.Events);
        Assert.Equal(command.VeaEventId.Id, _veaEvent.VeaEventId.Id);

    }

    [Fact]
    public async Task UpdateVisibility_Private()
    {
        // Arrange
        IUnitOfWork uow = new FakeUoW();
        ICommandHandler<UpdateEventVisibilityCommand> handler = new UpdateEventVisibilityHandler(repo, uow);

        UpdateEventVisibilityCommand command =
            UpdateEventVisibilityCommand.Create(_veaEvent.VeaEventId.Id, false).payload;

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        Assert.True(result.isSuccess);
        Assert.Single(repo.Events);
        Assert.Equal(command.VeaEventId.Id, _veaEvent.VeaEventId.Id);

    }
}
