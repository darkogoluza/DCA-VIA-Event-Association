using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.Event.UpdateVisibility;

public class EventVisibilityPrivateUnitTests
{
    private readonly VeaEvent VeaEvent;

    public EventVisibilityPrivateUnitTests()
    {
        var expectedTitleResult = Title.Create("Working Title");
        var expectedDescriptionResult = Description.Create("Some description");
        DateTime start = new DateTime(2025, 3, 4, 12, 0, 0);
        DateTime end = new DateTime(2025, 3, 4, 13, 0, 0);
        VeaEvent = VeaEvent.Create(expectedTitleResult.payload, expectedDescriptionResult.payload, start, end).payload;
    }

    [Fact]
    public void MakeEventPrivate_DraftState()
    {
        // Arrange
        // None

        // Act
        var changeVisibilityResult = VeaEvent.SetVisibility(false);

        // Assert
        Assert.True(changeVisibilityResult.isSuccess);
        Assert.False(VeaEvent._visibility);
    }

    [Fact]
    public void MakeEventPrivate_ReadyState()
    {
        // Arrange
        VeaEvent.Readie();

        // Act
        var changeVisibilityResult = VeaEvent.SetVisibility(false);

        // Assert
        Assert.True(changeVisibilityResult.isSuccess);
        Assert.False(VeaEvent._visibility);
    }

    [Fact]
    public void MakeEventPrivate_DraftStateAndPublic()
    {
        // Arrange
        VeaEvent.SetVisibility(true);

        // Act
        var changeVisibilityResult = VeaEvent.SetVisibility(false);

        // Assert
        Assert.True(changeVisibilityResult.isSuccess);
        Assert.False(VeaEvent._visibility);
    }

    [Fact]
    public void MakeEventPrivate_ReadyStateAndPublic()
    {
        // Arrange
        VeaEvent.Readie();
        VeaEvent.SetVisibility(true);

        // Act
        var changeVisibilityResult = VeaEvent.SetVisibility(false);

        // Assert
        Assert.True(changeVisibilityResult.isSuccess);
        Assert.False(VeaEvent._visibility);
    }

    [Fact]
    public void MakeEventPrivate_CancelledState()
    {
        // Arrange
        VeaEvent.SetVisibility(true);
        VeaEvent.Cancel();

        // Act
        var changeVisibilityResult = VeaEvent.SetVisibility(false);

        // Assert
        Assert.True(changeVisibilityResult.isFailure);
        Assert.Contains(Error.CanNotModifyCancelledEvent(), changeVisibilityResult.errors);
        Assert.True(VeaEvent._visibility);
    }

    [Fact]
    public void MakeEventPrivate_ActiveState()
    {
        // Arrange
        VeaEvent.SetVisibility(true);
        VeaEvent.Activate();

        // Act
        var changeVisibilityResult = VeaEvent.SetVisibility(false);

        // Assert
        Assert.True(changeVisibilityResult.isFailure);
        Assert.Contains(Error.CanNotModifyActiveEvent(), changeVisibilityResult.errors);
        Assert.True(VeaEvent._visibility);
    }
}
