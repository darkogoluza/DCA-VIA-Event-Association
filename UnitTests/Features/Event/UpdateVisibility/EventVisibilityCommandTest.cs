using ViaEventAssociation.Core.Application.Features.Event.UpdateVisibility;

namespace UnitTests.Features.Event.UpdateVisibility;

public class EventVisibilityCommandTest
{
    [Fact]
    public void UpdateVisibilityCommand_ToTrue_Success()
    {
        // Arrange
        bool visibility = true;

        // Act
        var result = UpdateEventVisibilityCommand.Create(Guid.NewGuid(), visibility);

        // Assert
        Assert.True(result.isSuccess);
        Assert.Equal(visibility, result.payload.Visibility);
    }

    [Fact]
    public void UpdateVisibilityCommand_ToFalse_Success()
    {
        // Arrange
        bool visibility = false;

        // Act
        var result = UpdateEventVisibilityCommand.Create(Guid.NewGuid(), visibility);

        // Assert
        Assert.True(result.isSuccess);
        Assert.Equal(visibility, result.payload.Visibility);
    }
}
