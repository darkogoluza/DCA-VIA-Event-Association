using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Values;
using VIAEventAssociation.Core.Domain.Common.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.Event.UpdateVisibility;

public class EventVisibilityPublicAggregateUnitTests
{
    private readonly VeaEvent VeaEvent;

    public EventVisibilityPublicAggregateUnitTests()
    {
        // Arrange
        var expectedTitleResult = Title.Create("Working Title");
        var expectedDescriptionResult = Description.Create("Some description");
        var expectedMaxNoOfGuestsResult = MaxNoOfGuests.Create(5);

        // Act
        VeaEvent = VeaEvent.Create().payload;
        VeaEvent._title = expectedTitleResult.payload;
        VeaEvent._description = expectedDescriptionResult.payload;
        VeaEvent._startDateTime = new DateTime(2025, 3, 4, 12, 0, 0);
        VeaEvent._endDateTime = new DateTime(2025, 3, 4, 13, 0, 0);
        VeaEvent._visibility = false;
        VeaEvent._maxNoOfGuests = expectedMaxNoOfGuestsResult.payload;

        // Assert
        Assert.NotEmpty(VeaEvent.VeaEventId.Id.ToString());
        Assert.Equal(EventStatusType.Draft, VeaEvent._eventStatusType);
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
        DateTime CurrentDateTimeMock() => new DateTime(2025, 3, 3, 12, 0, 0);
        VeaEvent.Readie(CurrentDateTimeMock);

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
