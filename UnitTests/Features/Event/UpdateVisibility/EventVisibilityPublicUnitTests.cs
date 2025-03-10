using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.Event.UpdateVisibility;

public class EventVisibilityPublicUnitTests
{
    private readonly VeaEvent VeaEvent;

    public EventVisibilityPublicUnitTests()
    {
        var expectedTitleResult = Title.Create("Working Title");
        var expectedDescriptionResult = Description.Create("Some description");
        DateTime start = new DateTime(2025, 3, 4, 12, 0, 0);
        DateTime end = new DateTime(2025, 3, 4, 13, 0, 0);
        VeaEvent = VeaEvent.Create(expectedTitleResult.payload, expectedDescriptionResult.payload, start, end).payload;
    }

    [Fact]
    public void MakeEventPublic_DraftState()
    {
        // Arrange

        // Act
        var changeVisibilityResult = VeaEvent.SetVisibility(true);

        // Assert
        Assert.True(changeVisibilityResult.isSuccess);
        Assert.True(VeaEvent._visibility);
    }

    [Fact]
    public void MakeEventPublic_ReadyState()
    {
        // Arrange
        VeaEvent.Readie();

        // Act
        var changeVisibilityResult = VeaEvent.SetVisibility(true);

        // Assert
        Assert.True(changeVisibilityResult.isSuccess);
        Assert.True(VeaEvent._visibility);
    }

    [Fact]
    public void MakeEventPublic_ActiveState()
    {
        // Arrange
        VeaEvent.Activate();

        // Act
        var changeVisibilityResult = VeaEvent.SetVisibility(true);

        // Assert
        Assert.True(changeVisibilityResult.isSuccess);
        Assert.True(VeaEvent._visibility);
    }

    [Fact]
    public void MakeEventPublic_CancelledState()
    {
        // Arrange
        VeaEvent.Cancel();

        // Act
        var changeVisibilityResult = VeaEvent.SetVisibility(true);

        // Assert
        Assert.True(changeVisibilityResult.isFailure);
        Assert.Contains(Error.CanNotModifyCancelledEvent(), changeVisibilityResult.errors);
        Assert.False(VeaEvent._visibility);
    }
}
