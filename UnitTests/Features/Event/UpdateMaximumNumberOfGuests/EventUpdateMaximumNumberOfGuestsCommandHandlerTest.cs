using UnitTests.Fakes;
using UnitTests.Stubs;
using ViaEventAssociation.Core.Application.Common.CommandHandler;
using ViaEventAssociation.Core.Application.Features.Event.CreateEvent;
using ViaEventAssociation.Core.Application.Features.Event.UpdateMaxNoOfGuests;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;
using VIAEventAssociation.Core.Domain.Common.Repositories;

namespace UnitTests.Features.Event.UpdateMaximumNumberOfGuests;

public class EventUpdateMaximumNumberOfGuestsCommandHandlerTest
{
    private readonly InMemEventRepoStub repo = new();
    private VeaEvent _veaEvent;

    public EventUpdateMaximumNumberOfGuestsCommandHandlerTest()
    {
        IUnitOfWork uow = new FakeUoW();
        ICommandHandler<CreateEventCommand> handler = new CreateEventHandler(repo, uow);

        CreateEventCommand command = CreateEventCommand.Create().payload;
        handler.HandleAsync(command);
        _veaEvent = repo.Events[0];
    }

    [Fact]
    public async Task UpdateMaxNoOfGuests()
    {
        // Arrange
        IUnitOfWork uow = new FakeUoW();
        ICommandHandler<UpdateEventMaxNoOfGuestsCommand> handler = new UpdateEventMaxNoOfGuestsHandler(repo, uow);

        UpdateEventMaxNoOfGuestsCommand command = UpdateEventMaxNoOfGuestsCommand.Create(_veaEvent.VeaEventId.Id, 10).payload;

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        Assert.True(result.isSuccess);
        Assert.Single(repo.Events);
        Assert.Equal(command.VeaEventId.Id, _veaEvent.VeaEventId.Id);
    }
}
