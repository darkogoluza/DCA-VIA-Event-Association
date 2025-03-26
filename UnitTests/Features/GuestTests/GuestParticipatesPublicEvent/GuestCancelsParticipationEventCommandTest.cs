using ViaEventAssociation.Core.Application.Features.Event.GuestCancelsParticipationEvent;

namespace UnitTests.Features.GuestTests.GuestParticipatesPublicEvent;

public class GuestCancelsParticipationEventCommandTest
{
    private DateTime CurrentDateTimeMock() => new DateTime(2025, 3, 3, 12, 0, 0);

    [Fact]
    public void CancelsParticipate()
    {
        // Arrange
        Guid guid1 = Guid.NewGuid();
        Guid guid2 = Guid.NewGuid();

        // Act
        var result = GuestCancelsParticipationEventCommand.Create(guid1, guid2);

        // Assert
        Assert.True(result.isSuccess);
        Assert.Equal(guid1, result.payload.VeaEventId.Id);
        Assert.Equal(guid2, result.payload.GuestId.Id);
    }

    [Fact]
    public void CancelsParticipate_WithMock()
    {
        // Arrange
        Guid guid1 = Guid.NewGuid();
        Guid guid2 = Guid.NewGuid();

        // Act
        var result = GuestCancelsParticipationEventCommand.Create(guid1, guid2, CurrentDateTimeMock);

        // Assert
        Assert.True(result.isSuccess);
        Assert.Equal(guid1, result.payload.VeaEventId.Id);
        Assert.Equal(guid2, result.payload.GuestId.Id);
    }
}