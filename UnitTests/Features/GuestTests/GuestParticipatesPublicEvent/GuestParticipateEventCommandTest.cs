using ViaEventAssociation.Core.Application.Features.Event.GuestParticipateEvent;

namespace UnitTests.Features.GuestTests.GuestParticipatesPublicEvent;

public class GuestParticipateEventCommandTest
{
    [Fact]
    public void Participate()
    {
        // Arrange
        Guid guid1 = Guid.NewGuid();
        Guid guid2 = Guid.NewGuid();

        // Act
        var result = GuestParticipateEventCommand.Create(guid1, guid2);

        // Assert
        Assert.True(result.isSuccess);
        Assert.Equal(guid1, result.payload.VeaEventId.Id);
        Assert.Equal(guid2, result.payload.GuestId.Id);
    }
}
