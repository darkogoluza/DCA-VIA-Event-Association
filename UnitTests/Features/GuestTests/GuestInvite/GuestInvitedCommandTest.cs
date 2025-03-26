using ViaEventAssociation.Core.Application.Features.Invitation;
using ViaEventAssociation.Core.Application.Features.Invitation.Invate;

namespace UnitTests.Features.GuestTests.GuestInvite;

public class GuestInvitedCommandTest
{
    [Fact]
    public void Invite()
    {
        // Arrange
        Guid guid1 = Guid.NewGuid();
        Guid guid2 = Guid.NewGuid();

        // Act
        var result = GuestInvitedCommand.Create(guid1, guid2);

        // Assert
        Assert.True(result.isSuccess);
        Assert.Equal(guid1, result.payload.VeaEventId.Id);
        Assert.Equal(guid2, result.payload.GuestId.Id);
    }
}
