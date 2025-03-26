using ViaEventAssociation.Core.Application.Features.Invitation.AcceptInvitation;

namespace UnitTests.Features.GuestTests.GuestDeclinesInvitation;

public class GuestDeclinesInvitationCommandTest
{
    [Fact]
    public void DeclineInvite()
    {
        // Arrange
        Guid guid1 = Guid.NewGuid();
        Guid guid2 = Guid.NewGuid();

        // Act
        var result = GuestAcceptsInvitationCommand.Create(guid1, guid2);

        // Assert
        Assert.True(result.isSuccess);
        Assert.Equal(guid1, result.payload.VeaEventId.Id);
        Assert.Equal(guid2, result.payload.GuestId.Id);
    }
}
