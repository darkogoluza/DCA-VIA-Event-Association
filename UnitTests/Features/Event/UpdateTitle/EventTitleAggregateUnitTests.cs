using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Values;
using VIAEventAssociation.Core.Domain.Common.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.Event.UpdateTitle;

public class EventTitleAggregateUnitTests
{
    private readonly VeaEvent VeaEvent;
    private DateTime CurrentDateTimeMock() => new DateTime(2025, 3, 3, 12, 0, 0);

    public EventTitleAggregateUnitTests()
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

    [Theory]
    [InlineData("Scary Movie Night!")]
    [InlineData("Graduation Gala")]
    [InlineData("VIA Hackathon")]
    public void UpdateEventTitle_DraftStatus(string newTitle)
    {
        // Arrange
        var newTitleResult = Title.Create(newTitle);

        // Act
        var newVeaEventResult = VeaEvent.UpdateTitle(newTitleResult.payload);

        // Assert
        Assert.True(newTitleResult.isSuccess);
        Assert.True(newVeaEventResult.isSuccess);
        Assert.Equal(newTitle, VeaEvent._title?.Value);
        Assert.Equal(EventStatusType.Draft, VeaEvent._eventStatusType);
    }

    [Theory]
    [InlineData("Scary Movie Night!")]
    [InlineData("Graduation Gala")]
    [InlineData("VIA Hackathon")]
    public void UpdateEventTitle_ReadyStatus(string newTitle)
    {
        // Arrange
        var newTitleResult = Title.Create(newTitle);

        // Act
        VeaEvent.Readie(CurrentDateTimeMock);
        var newVeaEventResult = VeaEvent.UpdateTitle(newTitleResult.payload);

        // Assert
        Assert.True(newTitleResult.isSuccess);
        Assert.True(newVeaEventResult.isSuccess);
        Assert.Equal(newTitle, VeaEvent._title?.Value);
        Assert.Equal(EventStatusType.Ready, VeaEvent._eventStatusType);
    }

    [Fact]
    public void UpdateEventTitle_TitleIsEmpty()
    {
        // Arrange
        var emptyTitle = "";

        // Act
        var newTitleResult = Title.Create(emptyTitle);

        // Assert
        Assert.True(newTitleResult.isFailure);
        Assert.Contains(Error.BadTitle(), newTitleResult.errors);
    }

    [Theory]
    [InlineData("XY")]
    [InlineData("a")]
    public void UpdateEventTitle_TitleIsToShort(string newTitle)
    {
        // Arrange
        // none

        // Act
        var newTitleResult = Title.Create(newTitle);

        // Assert
        Assert.True(newTitleResult.isFailure);
        Assert.Contains(Error.BadTitle(), newTitleResult.errors);
    }

    [Fact]
    public void UpdateEventTitle_TitleIsToLong()
    {
        // Arrange
        var toLongString = new string('A', 76);

        // Act
        var newTitleResult = Title.Create(toLongString);

        // Assert
        Assert.True(newTitleResult.isFailure);
        Assert.Contains(Error.BadTitle(), newTitleResult.errors);
    }

    [Fact]
    public void UpdateEventTitle_TitleIsNull()
    {
        // Arrange
        // None

        // Act
        var newTitleResult = Title.Create(null);

        // Assert
        Assert.True(newTitleResult.isFailure);
        Assert.Contains(Error.BadTitle(), newTitleResult.errors);
    }

    [Fact]
    public void UpdateEventTitle_ActiveState()
    {
        // Arrange
        var newTitleResult = Title.Create("new vea event title.");

        // Act
        VeaEvent.Readie(CurrentDateTimeMock);
        VeaEvent.Activate();
        var newVeaEventResult = VeaEvent.UpdateTitle(newTitleResult.payload);

        // Assert
        Assert.True(newTitleResult.isSuccess);
        Assert.True(newVeaEventResult.isFailure);
        Assert.Contains(Error.CanNotModifyActiveEvent(), newVeaEventResult.errors);
        Assert.Equal("Working Title", VeaEvent._title?.Value);
    }

    [Fact]
    public void UpdateEventTitle_CancelledState()
    {
        // Arrange
        var newTitleResult = Title.Create("new vea event title.");

        // Act
        VeaEvent.Cancel();
        var newVeaEventResult = VeaEvent.UpdateTitle(newTitleResult.payload);

        // Assert
        Assert.True(newTitleResult.isSuccess);
        Assert.True(newVeaEventResult.isFailure);
        Assert.Contains(Error.CanNotModifyCancelledEvent(), newVeaEventResult.errors);
        Assert.Equal("Working Title", VeaEvent._title?.Value);
    }
}
