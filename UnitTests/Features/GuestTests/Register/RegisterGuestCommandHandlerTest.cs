using UnitTests.Fakes;
using UnitTests.Stubs;
using ViaEventAssociation.Core.Application.Common.CommandHandler;
using ViaEventAssociation.Core.Application.Features.Guest.RegisterGuest;
using VIAEventAssociation.Core.Domain.Common.Repositories;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.GuestTests.Register;

public class RegisterGuestCommandHandlerTest
{
    private readonly InMemGuestRepoStub repo = new();

    [Fact]
    public async void RegisterGuestCommandHandler_Success()
    {
        // Arrange 
        ICommandHandler<RegisterGuestCommand> handler = new RegisterGuestHandler(repo);

        RegisterGuestCommand command = RegisterGuestCommand.Create("John", "Doe", "jhd@via.dk",
                "https://media.istockphoto.com/id/521573873/vector/unknown-person-silhouette-whith-blue-tie.jpg?s=2048x2048&w=is&k=20&c=cjOrS4d7gV46uXDx9iWH5n5uSEF6hhZ6Gebbp5j6USI=")
            .payload;

        // Act
        Result<None> result = await handler.HandleAsync(command);

        // Assert
        Assert.True(result.isSuccess);
        Assert.Single(repo.Guests);
    }
}
