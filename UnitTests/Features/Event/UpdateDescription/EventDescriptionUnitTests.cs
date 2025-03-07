using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Values;
using VIAEventAssociation.Core.Domain.Common.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.Event.UpdateDescription;

public class EventDescriptionUnitTests
{
    private readonly VeaEvent VeaEvent;

    private readonly string descriptionConst =
        "Nullam tempor lacus nisl, eget tempus quam maximus malesuada. Morbi faucibus sed neque vitae euismod. Vestibulum non purus vel justo ornare vulputate. In a interdum enim. Maecenas sed sodales elit, sit amet venenatis orci. Suspendisse potenti";

    public EventDescriptionUnitTests()
    {
        var expectedTitleResult = Title.Create("Working Title");
        var expectedDescriptionResult = Description.Create("Some description");
        DateTime start = new DateTime(2025, 3, 4, 12, 0, 0);
        DateTime end = new DateTime(2025, 3, 4, 13, 0, 0);
        VeaEvent = VeaEvent.Create(expectedTitleResult.payload, expectedDescriptionResult.payload, start, end).payload;
    }

    [Fact]
    public void UpdateEventDescription_DraftStatus()
    {
        // Arrange
        var newDescriptionResult = Description.Create(descriptionConst);

        // Act
        var newVeaEventResult = VeaEvent.UpdateDescription(newDescriptionResult.payload);

        // Assert
        Assert.True(newDescriptionResult.isSuccess);
        Assert.True(newVeaEventResult.isSuccess);
        Assert.Equal(descriptionConst, VeaEvent._description.Value);
        Assert.Equal(EventStatusType.Draft, VeaEvent._eventStatusType);
    }

    [Fact]
    public void UpdateEventDescription_DraftStatus_EmptyDescription()
    {
        // Arrange
        var newDescription = "";
        var newDescriptionResult = Description.Create(newDescription);

        // Act
        var newVeaEventResult = VeaEvent.UpdateDescription(newDescriptionResult.payload);

        // Assert
        Assert.True(newDescriptionResult.isSuccess);
        Assert.True(newVeaEventResult.isSuccess);
        Assert.Equal(newDescription, VeaEvent._description.Value);
        Assert.Equal(EventStatusType.Draft, VeaEvent._eventStatusType);
    }

    [Fact]
    public void UpdateEventDescription_ReadyStatus()
    {
        // Arrange
        var newDescriptionResult = Description.Create(descriptionConst);

        // Act
        VeaEvent.Readie();
        var newVeaEventResult = VeaEvent.UpdateDescription(newDescriptionResult.payload);

        // Assert
        Assert.True(newDescriptionResult.isSuccess);
        Assert.True(newVeaEventResult.isSuccess);
        Assert.Equal(descriptionConst, VeaEvent._description.Value);
        Assert.Equal(EventStatusType.Ready, VeaEvent._eventStatusType);
    }

    [Fact]
    public void UpdateEventDescription_ReadyStatus_EmptyDescription()
    {
        // Arrange
        var newDescription = "";
        var newDescriptionResult = Description.Create(newDescription);

        // Act
        VeaEvent.Readie();
        var newVeaEventResult = VeaEvent.UpdateDescription(newDescriptionResult.payload);

        // Assert
        Assert.True(newDescriptionResult.isSuccess);
        Assert.True(newVeaEventResult.isSuccess);
        Assert.Equal(newDescription, VeaEvent._description.Value);
        Assert.Equal(EventStatusType.Ready, VeaEvent._eventStatusType);
    }

    [Fact]
    public void UpdateEventDescription_DescriptionIsToLong()
    {
        // Arrange
        var newDescription = new string('A', 251);

        // Act
        var newDescriptionResult = Description.Create(newDescription);

        // Assert
        Assert.True(newDescriptionResult.isFailure);
        Assert.Contains(Error.BadDescription(), newDescriptionResult.errors);
    }

    [Fact]
    public void UpdateEventDescription_ActiveStatus()
    {
        // Arrange
        var newDescriptionResult = Description.Create(descriptionConst);

        // Act
        VeaEvent.Activate();
        var newVeaEventResult = VeaEvent.UpdateDescription(newDescriptionResult.payload);

        // Assert
        Assert.True(newDescriptionResult.isSuccess);
        Assert.True(newVeaEventResult.isFailure);
        Assert.Contains(Error.CanNotModifyActiveEvent(), newVeaEventResult.errors);
        Assert.Equal("Some description", VeaEvent._description.Value);
    }

    [Fact]
    public void UpdateEventDescription_CancelledStatus()
    {
        // Arrange
        var newDescriptionResult = Description.Create(descriptionConst);

        // Act
        VeaEvent.Cancel();
        var newVeaEventResult = VeaEvent.UpdateDescription(newDescriptionResult.payload);

        // Assert
        Assert.True(newDescriptionResult.isSuccess);
        Assert.True(newVeaEventResult.isFailure);
        Assert.Contains(Error.CanNotModifyCancelledEvent(), newVeaEventResult.errors);
        Assert.Equal("Some description", VeaEvent._description.Value);
    }
}
