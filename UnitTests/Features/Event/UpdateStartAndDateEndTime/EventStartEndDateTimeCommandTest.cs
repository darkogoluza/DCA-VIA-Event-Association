using ViaEventAssociation.Core.Application.Features.Event.UpdateStartAndEndDateTime;

namespace UnitTests.Features.Event.UpdateStartAndDateEndTime;

public class EventStartEndDateTimeCommandTest
{
    [Fact]
    public void UpdateStartAndEndDateTimeCommand()
    {
        // Arrange
        DateTime start = new DateTime(2025, 03, 25, 13, 0, 0);
        DateTime end = new DateTime(2025, 03, 25, 16, 0, 0);

        // Act
        var result = UpdateEventStartAndEndDateTimeCommand.Create(Guid.NewGuid(), start, end);

        // Assert
        Assert.True(result.isSuccess);
        Assert.Equal(start, result.payload.Start);
        Assert.Equal(end, result.payload.End);
    }
}
