using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Values;
using VIAEventAssociation.Core.Domain.Common.Values;
using VIAEventAssociation.Core.Domain.Contracts;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.Event.ReadieEvent;

public class ReadieEventAggregateUnitTests
{
    private readonly VeaEvent VeaEvent;

    public ReadieEventAggregateUnitTests()
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
    public void ReadieEvent()
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
        DateTime CurrentDateTimeMock() => new DateTime(2025, 3, 3, 12, 0, 0);

        // Act
        var redieResult = VeaEvent.Readie(CurrentDateTimeMock);

        // Assert
        Assert.True(redieResult.isSuccess);
        Assert.Equal(EventStatusType.Ready, VeaEvent._eventStatusType);
    }

    [Fact]
    public void ReadieEvent_CancelledState()
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
        VeaEvent.Cancel();

        DateTime CurrentDateTimeMock() => new DateTime(2025, 3, 3, 12, 0, 0);

        // Act
        var redieResult = VeaEvent.Readie(CurrentDateTimeMock);

        // Assert
        Assert.True(redieResult.isFailure);
        Assert.Equal(EventStatusType.Cancelled, VeaEvent._eventStatusType);
        Assert.Contains(Error.CanNotModifyCancelledEvent(), redieResult.errors);
    }

    [Fact]
    public void RedieEvent_NotSetElements()
    {
        // Arrange
        // None

        // Act
        var redieResult = VeaEvent.Readie();

        // Assert
        Assert.True(redieResult.isFailure);
        Assert.Equal(EventStatusType.Draft, VeaEvent._eventStatusType);
        Assert.Contains(Error.TitleNotSet(), redieResult.errors);
        Assert.Contains(Error.DescriptionNotSet(), redieResult.errors);
        Assert.Contains(Error.TimesAreNotSet(), redieResult.errors);
        Assert.Contains(Error.VisibilityIsNotSet(), redieResult.errors);
        Assert.Contains(Error.MaximumNumberOfGuestsIsNotSet(), redieResult.errors);
    }

    [Fact]
    public void RedieEvent_EventIsInThePast()
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

        DateTime CurrentDateTimeMock() => new DateTime(2025, 3, 5, 12, 0, 0);

        // Act
        var redieResult = VeaEvent.Readie(CurrentDateTimeMock);

        // Assert
        Assert.True(redieResult.isFailure);
        Assert.Contains(Error.EventIsInPast(), redieResult.errors);
    }
}
