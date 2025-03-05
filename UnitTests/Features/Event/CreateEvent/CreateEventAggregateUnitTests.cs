using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;
using VIAEventAssociation.Core.Domain.Common.Values;
using Xunit.Abstractions;

namespace UnitTests.Features.Event.CreateEvent;

public class CreateEventAggregateUnitTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public CreateEventAggregateUnitTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void CreateANewEvent_EventId()
    {
        // Arrange
        string expectedTitle = "Working Title";
        string expectedDescription = "Some description";
        DateTime start = new DateTime(2025, 3, 4, 12, 0, 0);
        DateTime end = new DateTime(2025, 3, 4, 13, 0, 0);

        // Act
        var result = VeaEvent.Create(expectedTitle, expectedDescription, start, end);

        // Assert
        Assert.True(result.isSuccess);
        var veaEvent = result.payload;
        Assert.NotEmpty(veaEvent.EventId.Id.ToString());
    }
    
    [Fact]
    public void CreateANewEvent_CreatedValuesAndDefaults()
    {
        // Arrange
        string expectedTitle = "Working Title";
        string expectedDescription = "Some description";
        DateTime start = new DateTime(2025, 3, 4, 12, 0, 0);
        DateTime end = new DateTime(2025, 3, 4, 13, 0, 0);

        // Act
        var result = VeaEvent.Create(expectedTitle, expectedDescription, start, end);

        // Assert
        Assert.True(result.isSuccess);
        var veaEvent = result.payload;
        Assert.Equal(expectedTitle, veaEvent._title.Value);
        Assert.Equal(expectedDescription, veaEvent._description.Value);
        Assert.False(veaEvent._visibility); // Test for default value
        Assert.Equal(5, veaEvent._maxNoOfGuests.Value); // Test for default value
        Assert.Equal(EventStatusType.Draft, veaEvent._eventStatusType); // Test for default value
    }
}
