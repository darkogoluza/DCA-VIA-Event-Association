using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Values;
using VIAEventAssociation.Core.Domain.Common.Values;
using Xunit.Abstractions;

namespace UnitTests.Features.Event.CreateEvent;

public class CreateEventAggregateUnitTests
{
    [Fact]
    public void CreateANewEvent_EventId()
    {
        // Arrange
        var expectedTitleResult = Title.Create("Working Title");
        var expectedDescriptionResult = Description.Create("Some description");
        DateTime start = new DateTime(2025, 3, 4, 12, 0, 0);
        DateTime end = new DateTime(2025, 3, 4, 13, 0, 0);

        // Act
        var result = VeaEvent.Create(expectedTitleResult.payload, expectedDescriptionResult.payload, start, end);

        // Assert
        Assert.True(result.isSuccess);
        var veaEvent = result.payload;
        Assert.NotEmpty(veaEvent.VeaEventId.Id.ToString());
    }

    [Fact]
    public void CreateANewEvent_CreatedValuesAndDefaults()
    {
        // Arrange
        var expectedTitleResult = Title.Create("Working Title");
        var expectedDescriptionResult = Description.Create("Some description");
        DateTime start = new DateTime(2025, 3, 4, 12, 0, 0);
        DateTime end = new DateTime(2025, 3, 4, 13, 0, 0);

        // Act
        var result = VeaEvent.Create(expectedTitleResult.payload, expectedDescriptionResult.payload, start, end);

        // Assert
        Assert.True(result.isSuccess);
        var veaEvent = result.payload;
        Assert.Equal(expectedTitleResult.payload.Value, veaEvent._title.Value);
        Assert.Equal(expectedDescriptionResult.payload.Value, veaEvent._description.Value);
        Assert.False(veaEvent._visibility); // Test for default value
        Assert.Equal(5, veaEvent._maxNoOfGuests.Value); // Test for default value
        Assert.Equal(EventStatusType.Draft, veaEvent._eventStatusType); // Test for default value
    }
}
