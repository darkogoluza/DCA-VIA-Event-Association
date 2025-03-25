using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Values;
using VIAEventAssociation.Core.Domain.Common.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.Event.ActivateEvent;

public class ActivateEventAggregateUnitTests
{
    private readonly VeaEvent VeaEvent;
    private DateTime CurrentDateTimeMock() => new DateTime(2025, 3, 3, 12, 0, 0);

    public ActivateEventAggregateUnitTests()
    {
        // Arrange
        // None

        // Act
        VeaEvent = VeaEvent.Create().payload;

        // Assert
        Assert.NotEmpty(VeaEvent.VeaEventId.Id.ToString());
        Assert.Equal(EventStatusType.Draft, VeaEvent._eventStatusType);
    }

    [Fact]
    public void ActivateEvent()
    {
        // Arrange
        var expectedTitleResult = Title.Create("Working Title");
        var expectedDescriptionResult = Description.Create("Some description");
        var expectedMaxNoOfGuestsResult = MaxNoOfGuests.Create(5);

        VeaEvent.UpdateTitle(expectedTitleResult.payload);
        VeaEvent.UpdateDescription(expectedDescriptionResult.payload);
        VeaEvent.SetMaxNoOfGuests(expectedMaxNoOfGuestsResult.payload);
        VeaEvent.UpdateStarEndDateTime(new DateTime(2025, 3, 4, 12, 0, 0), new DateTime(2025, 3, 4, 13, 0, 0));
        VeaEvent.SetVisibility(false);
        VeaEvent.Readie(CurrentDateTimeMock);

        // Act
        var activateEventResult = VeaEvent.Activate();

        // Assert
        Assert.True(activateEventResult.isSuccess);
        Assert.Equal(EventStatusType.Active, VeaEvent._eventStatusType);
    }

    [Fact]
    public void ActivateEvent_ActiveState()
    {
        // Arrange
        var expectedTitleResult = Title.Create("Working Title");
        var expectedDescriptionResult = Description.Create("Some description");
        var expectedMaxNoOfGuestsResult = MaxNoOfGuests.Create(5);

        VeaEvent.UpdateTitle(expectedTitleResult.payload);
        VeaEvent.UpdateDescription(expectedDescriptionResult.payload);
        VeaEvent.SetMaxNoOfGuests(expectedMaxNoOfGuestsResult.payload);
        VeaEvent.UpdateStarEndDateTime(new DateTime(2025, 3, 4, 12, 0, 0), new DateTime(2025, 3, 4, 13, 0, 0));
        VeaEvent.SetVisibility(false);
        VeaEvent.Readie(CurrentDateTimeMock);
        VeaEvent.Activate();

        // Act
        var activateEventResult = VeaEvent.Activate();

        // Assert
        Assert.True(activateEventResult.isSuccess);
        Assert.Equal(EventStatusType.Active, VeaEvent._eventStatusType);
    }

    // By forcing the event to be ready first, then we get all the validation from ready method for free
    [Fact]
    public void ActivateEvent_DraftState()
    {
        // Arrange
        // None

        // Act
        var activateEventResult = VeaEvent.Activate();

        // Assert
        Assert.True(activateEventResult.isFailure);
        Assert.Contains(Error.CanNotActivateEventThatIsNotReady(), activateEventResult.errors);
    }

    [Fact]
    public void ActivateEvent_CancelledState()
    {
        // Arrange
        VeaEvent.Cancel();

        // Act
        var activateEventResult = VeaEvent.Activate();

        // Assert
        Assert.True(activateEventResult.isFailure);
        Assert.Contains(Error.CanNotModifyCancelledEvent(), activateEventResult.errors);
    }
}
