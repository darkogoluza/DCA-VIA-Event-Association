using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Values;
using VIAEventAssociation.Core.Domain.Common.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.Event.UpdateTitle;

public class EventDescriptionUnitTests
{
    private readonly VeaEvent VeaEvent;

    public EventDescriptionUnitTests()
    {
        string expectedTitle = "Working Title";
        string expectedDescription = "Some description";
        DateTime start = new DateTime(2025, 3, 4, 12, 0, 0);
        DateTime end = new DateTime(2025, 3, 4, 13, 0, 0);
        VeaEvent = VeaEvent.Create(expectedTitle, expectedDescription, start, end).payload;
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
        Assert.Equal(newTitle, newVeaEventResult.payload._title.Value);
        Assert.Equal(EventStatusType.Draft, newVeaEventResult.payload._eventStatusType);
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
        VeaEvent.Readie();
        var newVeaEventResult = VeaEvent.UpdateTitle(newTitleResult.payload);

        // Assert
        Assert.True(newTitleResult.isSuccess);
        Assert.True(newVeaEventResult.isSuccess);
        Assert.Equal(newTitle, newVeaEventResult.payload._title.Value);
        Assert.Equal(EventStatusType.Ready, newVeaEventResult.payload._eventStatusType);
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
        VeaEvent.Activate();
        var newVeaEventResult = VeaEvent.UpdateTitle(newTitleResult.payload);

        // Assert
        Assert.True(newTitleResult.isSuccess);
        Assert.True(newVeaEventResult.isFailure);
        Assert.Contains(Error.CanNotModifyActiveEvent(), newVeaEventResult.errors);
    }
    
    [Fact]
    public void UpdateEventTitle_CancledState()
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
    }
}
