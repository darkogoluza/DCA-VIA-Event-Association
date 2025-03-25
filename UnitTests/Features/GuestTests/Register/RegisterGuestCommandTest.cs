using ViaEventAssociation.Core.Application.Features.Event.CreateEvent;
using ViaEventAssociation.Core.Application.Features.Guest.RegisterGuest;

namespace UnitTests.Features.GuestTests.Register;

public class RegisterGuestCommandTest
{
    [Fact]
    public void RegisterGuestCommand_Success()
    {
        // Arrange

        // Act
        var result = RegisterGuestCommand.Create("John", "Doe", "jhd@via.dk",
            "https://media.istockphoto.com/id/521573873/vector/unknown-person-silhouette-whith-blue-tie.jpg?s=2048x2048&w=is&k=20&c=cjOrS4d7gV46uXDx9iWH5n5uSEF6hhZ6Gebbp5j6USI=");

        // Assert
        Assert.True(result.isSuccess);
        Assert.NotEmpty(result.payload.Guest.GuestId.Id.ToString());
    }

    [Fact]
    public void RegisterGuestCommand_Error()
    {
        // Arrange

        // Act
        var result = RegisterGuestCommand.Create("J", "D", "jhd@gmail.com",
            "https://media.istockphoto.com/id/521573873/vector/unknown-person-silhouette-whith-blue-tie.jpg?s=2048x2048&w=is&k=20&c=cjOrS4d7gV46uXDx9iWH5n5uSEF6hhZ6Gebbp5j6USI=");

        // Assert
        Assert.True(result.isFailure);
    }
}
