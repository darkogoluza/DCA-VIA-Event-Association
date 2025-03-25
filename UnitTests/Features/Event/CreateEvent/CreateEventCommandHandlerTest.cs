using UnitTests.Fakes;
using UnitTests.Stubs;
using ViaEventAssociation.Core.Application.Common.CommandHandler;
using ViaEventAssociation.Core.Application.Features.Event.CreateEvent;
using VIAEventAssociation.Core.Domain.Common.Repositories;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.Event.CreateEvent;

public class CreateEventCommandHandlerTest
{
    private readonly InMemEventRepoStub repo = new();

    [Fact]
    public async void CreateEventCommandHandler_Success()
    {
        // Arrange 
        IUnitOfWork uow = new FakeUoW();
        ICommandHandler<CreateEventCommand> handler = new CreateEventHandler(repo, uow);

        CreateEventCommand command = CreateEventCommand.Create().payload;

        // Act
        Result<None> result = await handler.HandleAsync(command);

        // Assert
        Assert.True(result.isSuccess);
        Assert.Single(repo.Events);
    }
}
