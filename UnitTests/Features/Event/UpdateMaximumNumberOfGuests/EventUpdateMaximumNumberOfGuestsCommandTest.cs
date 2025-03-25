using ViaEventAssociation.Core.Application.Features.Event.UpdateMaxNoOfGuests;

namespace UnitTests.Features.Event.UpdateMaximumNumberOfGuests;

public class EventUpdateMaximumNumberOfGuestsCommandTest
{
    [Fact]
    public void UpdateMaxNoOfGuestsCommand_Success()
    {
        // Arrange
        int maxNoOfGuests = 10;

        // Act
        var result = UpdateEventMaxNoOfGuestsCommand.Create(Guid.NewGuid(), maxNoOfGuests);

        // Assert
        Assert.True(result.isSuccess);
        Assert.Equal(maxNoOfGuests, result.payload.MaxNoOfGuests.Value);
    }

    [Fact]
    public void UpdateMaxNoOfGuestsCommand_Error()
    {
        // Arrange
        int maxNoOfGuests = 3;

        // Act
        var result = UpdateEventMaxNoOfGuestsCommand.Create(Guid.NewGuid(), maxNoOfGuests);

        // Assert
        Assert.True(result.isFailure);
    }
}
