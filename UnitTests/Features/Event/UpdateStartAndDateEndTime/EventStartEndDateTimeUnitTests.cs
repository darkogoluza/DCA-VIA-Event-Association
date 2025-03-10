using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Values;
using VIAEventAssociation.Core.Domain.Common.Values;
using ViaEventAssociation.Core.Tools.OperationResult;
using Xunit.Abstractions;

namespace UnitTests.Features.Event.UpdateStartAndDateEndTime;

public class EventStartEndDateTimeUnitTests
{
    private readonly VeaEvent VeaEvent;
    private readonly DateTime DefaultStartDateTime = new DateTime(2023, 3, 4, 12, 0, 0);
    private readonly DateTime DefaultEndDateTime = new DateTime(2023, 3, 4, 14, 0, 0);

    public EventStartEndDateTimeUnitTests()
    {
        // Arrange
        var expectedTitleResult = Title.Create("Working Title");
        var expectedDescriptionResult = Description.Create("Some description");
        var expectedMaxNoOfGuestsResult = MaxNoOfGuests.Create(5);

        // Act
        VeaEvent = VeaEvent.Create().payload;
        VeaEvent._title = expectedTitleResult.payload;
        VeaEvent._description = expectedDescriptionResult.payload;
        VeaEvent._startDateTime = DefaultStartDateTime;
        VeaEvent._endDateTime = DefaultEndDateTime;
        VeaEvent._visibility = false;
        VeaEvent._maxNoOfGuests = expectedMaxNoOfGuestsResult.payload;

        // Assert
        Assert.NotEmpty(VeaEvent.VeaEventId.Id.ToString());
        Assert.Equal(EventStatusType.Draft, VeaEvent._eventStatusType);
    }

    [Theory]
    [MemberData(nameof(EventStartEndDateTimeData.EventUpdateStartEndTimeData_S1),
        MemberType = typeof(EventStartEndDateTimeData))]
    public void EventUpdateStartEndTime_S1(DateTime start, DateTime end)
    {
        // Arrange
        // None

        // Act
        var updateStartEndDateTimeResult = VeaEvent.UpdateStarEndDateTime(start, end);

        // Assert
        Assert.True(updateStartEndDateTimeResult.isSuccess);
        Assert.Equal(start, VeaEvent._startDateTime);
        Assert.Equal(end, VeaEvent._endDateTime);
    }

    [Theory]
    [MemberData(nameof(EventStartEndDateTimeData.EventUpdateStartEndTimeData_S2),
        MemberType = typeof(EventStartEndDateTimeData))]
    public void EventUpdateStartEndDateTime_S2(DateTime start, DateTime end)
    {
        // Arrange
        // None

        // Act
        var updateStartEndDateTimeResult = VeaEvent.UpdateStarEndDateTime(start, end);

        // Assert
        Assert.True(updateStartEndDateTimeResult.isSuccess);
        Assert.Equal(start, VeaEvent._startDateTime);
        Assert.Equal(end, VeaEvent._endDateTime);
    }

    [Theory]
    [MemberData(nameof(EventStartEndDateTimeData.EventUpdateStartEndTimeData_S1),
        MemberType = typeof(EventStartEndDateTimeData))]
    [MemberData(nameof(EventStartEndDateTimeData.EventUpdateStartEndTimeData_S2),
        MemberType = typeof(EventStartEndDateTimeData))]
    public void EventUpdateStartEndDateTime_ReadyState(DateTime start, DateTime end)
    {
        // Arrange
        DateTime CurrentDateTimeMock() => DefaultStartDateTime.AddDays(-1);

        // Act
        var updateToReadyState = VeaEvent.Readie(CurrentDateTimeMock);
        var updateStartEndDateTimeResult = VeaEvent.UpdateStarEndDateTime(start, end);

        // Assert

        Assert.True(updateToReadyState.isSuccess);
        Assert.True(updateStartEndDateTimeResult.isSuccess);
        Assert.Equal(start, VeaEvent._startDateTime);
        Assert.Equal(end, VeaEvent._endDateTime);
        Assert.Equal(EventStatusType.Ready, VeaEvent._eventStatusType);
    }

    [Theory]
    [MemberData(nameof(EventStartEndDateTimeData.EventUpdateStartEndTimeData_F1_StartDateAfterEndDate),
        MemberType = typeof(EventStartEndDateTimeData))]
    public void EventUpdateStartEndDateTime_StartDateIsAfterEndDate(DateTime start, DateTime end)
    {
        // Arrange
        // None

        // Act
        var updateStartEndDateTimeResult = VeaEvent.UpdateStarEndDateTime(start, end);

        // Assert
        Assert.True(updateStartEndDateTimeResult.isFailure);
        Assert.Equal(DefaultStartDateTime, VeaEvent._startDateTime);
        Assert.Equal(DefaultEndDateTime, VeaEvent._endDateTime); // Check that the value did not update
        Assert.Contains(Error.StartDateTimeIsBiggerThenEndDateTime(), updateStartEndDateTimeResult.errors);
    }

    [Theory]
    [MemberData(nameof(EventStartEndDateTimeData.EventUpdateStartEndTimeData_F2_StartTimeAfterEndTime),
        MemberType = typeof(EventStartEndDateTimeData))]
    public void EventUpdateStartEndDateTime_StartTimeIsAfterEndTime(DateTime start, DateTime end)
    {
        // Arrange
        // None

        // Act
        var updateStartEndDateTimeResult = VeaEvent.UpdateStarEndDateTime(start, end);

        // Assert
        Assert.True(updateStartEndDateTimeResult.isFailure);
        Assert.Equal(DefaultStartDateTime, VeaEvent._startDateTime);
        Assert.Equal(DefaultEndDateTime, VeaEvent._endDateTime); // Check that the value did not update
        Assert.Contains(Error.StartDateTimeIsBiggerThenEndDateTime(), updateStartEndDateTimeResult.errors);
    }

    [Theory]
    [MemberData(nameof(EventStartEndDateTimeData.EventUpdateStartEndTimeData_F3_EventDurationTooShort),
        MemberType = typeof(EventStartEndDateTimeData))]
    [MemberData(nameof(EventStartEndDateTimeData.EventUpdateStartEndTimeData_F4_EventDurationTooShort),
        MemberType = typeof(EventStartEndDateTimeData))]
    public void EventUpdateStartEndDateTime_EventDurationTooShort(DateTime start, DateTime end)
    {
        // Arrange
        // None

        // Act
        var updateStartEndDateTimeResult = VeaEvent.UpdateStarEndDateTime(start, end);

        // Assert
        Assert.True(updateStartEndDateTimeResult.isFailure);
        Assert.Equal(DefaultStartDateTime, VeaEvent._startDateTime);
        Assert.Equal(DefaultEndDateTime, VeaEvent._endDateTime); // Check that the value did not update
        Assert.Contains(Error.EventDurationTooShort(), updateStartEndDateTimeResult.errors);
    }

    [Theory]
    [MemberData(nameof(EventStartEndDateTimeData.EventUpdateStartEndTimeData_F5_EventStartTimeToEarly),
        MemberType = typeof(EventStartEndDateTimeData))]
    public void EventUpdateStartEndDateTime_EventStartTimeTooEarly(DateTime start, DateTime end)
    {
        // Arrange
        // None

        // Act
        var updateStartEndDateTimeResult = VeaEvent.UpdateStarEndDateTime(start, end);

        // Assert
        Assert.True(updateStartEndDateTimeResult.isFailure);
        Assert.Equal(DefaultStartDateTime, VeaEvent._startDateTime); // Check that the value did not update
        Assert.Contains(Error.StartTimeTooEarly(), updateStartEndDateTimeResult.errors);
    }

    [Theory]
    [MemberData(nameof(EventStartEndDateTimeData.EventUpdateStartEndTimeData_F6_EventStartTimeToEarly),
        MemberType = typeof(EventStartEndDateTimeData))]
    public void EventUpdateStartEndDateTime_EventEndTimeTooLate(DateTime start, DateTime end)
    {
        // Arrange
        // None

        // Act
        var updateStartEndDateTimeResult = VeaEvent.UpdateStarEndDateTime(start, end);

        // Assert
        Assert.True(updateStartEndDateTimeResult.isFailure);
        Assert.Equal(DefaultStartDateTime, VeaEvent._startDateTime);
        Assert.Equal(DefaultEndDateTime, VeaEvent._endDateTime); // Check that the value did not update
        Assert.Contains(Error.EndTimeTooLate(), updateStartEndDateTimeResult.errors);
    }

    [Fact]
    public void EventUpdateStartEndDateTime_ActiveState()
    {
        // Arrange
        VeaEvent.Activate();

        // Act
        var updateStartEndDateTimeResult = VeaEvent.UpdateStarEndDateTime(DefaultStartDateTime, DefaultEndDateTime);

        // Assert
        Assert.True(updateStartEndDateTimeResult.isFailure);
        Assert.Contains(Error.CanNotModifyActiveEvent(), updateStartEndDateTimeResult.errors);
    }

    [Fact]
    public void EventUpdateStartEndDateTime_CancelledState()
    {
        // Arrange
        VeaEvent.Cancel();

        // Act
        var updateStartEndDateTimeResult = VeaEvent.UpdateStarEndDateTime(DefaultStartDateTime, DefaultEndDateTime);

        // Assert
        Assert.True(updateStartEndDateTimeResult.isFailure);
        Assert.Contains(Error.CanNotModifyCancelledEvent(), updateStartEndDateTimeResult.errors);
    }

    [Theory]
    [MemberData(nameof(EventStartEndDateTimeData.EventUpdateStartEndTimeData_F9_EventDurationTooLong),
        MemberType = typeof(EventStartEndDateTimeData))]
    public void EventUpdateStartEndDateTime_EventDurationTooLong(DateTime start, DateTime end)
    {
        // Arrange
        // None

        // Act
        var updateStartEndDateTimeResult = VeaEvent.UpdateStarEndDateTime(start, end);

        // Assert
        Assert.True(updateStartEndDateTimeResult.isFailure);
        Assert.Equal(DefaultStartDateTime, VeaEvent._startDateTime);
        Assert.Equal(DefaultEndDateTime, VeaEvent._endDateTime); // Check that the value did not update
        Assert.Contains(Error.EventDurationTooLong(), updateStartEndDateTimeResult.errors);
    }

    [Fact]
    public void EventUpdateStartEndDateTime_EventStartTimeInPast()
    {
        // Arrange
        // None

        // Act
        var updateStartEndDateTimeResult = VeaEvent.UpdateStarEndDateTime(DefaultStartDateTime.AddHours(-1), DefaultEndDateTime);

        // Assert
        Assert.True(updateStartEndDateTimeResult.isFailure);
        Assert.Equal(DefaultStartDateTime, VeaEvent._startDateTime);
        Assert.Equal(DefaultEndDateTime, VeaEvent._endDateTime); // Check that the value did not update
        Assert.Contains(Error.StartTimeInPast(), updateStartEndDateTimeResult.errors);
    }

    [Theory]
    [MemberData(nameof(EventStartEndDateTimeData.EventUpdateStartEndTimeData_F11_EventDurationSpansInvalidTime),
        MemberType = typeof(EventStartEndDateTimeData))]
    public void EventUpdateStartEndDateTime_EventDurationSpansInvalidTime(DateTime start, DateTime end)
    {
        // Arrange
        // None

        // Act
        var updateStartEndDateTimeResult = VeaEvent.UpdateStarEndDateTime(start, end);

        // Assert
        Assert.True(updateStartEndDateTimeResult.isFailure);
        Assert.Equal(DefaultStartDateTime, VeaEvent._startDateTime);
        Assert.Equal(DefaultEndDateTime, VeaEvent._endDateTime); // Check that the value did not update
        Assert.Contains(Error.InvalidTimeSpan(), updateStartEndDateTimeResult.errors);
    }
}
