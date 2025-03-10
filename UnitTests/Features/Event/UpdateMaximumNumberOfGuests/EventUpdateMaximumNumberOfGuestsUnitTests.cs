﻿using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.Event.UpdateMaximumNumberOfGuests;

public class EventUpdateMaximumNumberOfGuestsUnitTests
{
    private readonly VeaEvent VeaEvent;

    public EventUpdateMaximumNumberOfGuestsUnitTests()
    {
        var expectedTitleResult = Title.Create("Working Title");
        var expectedDescriptionResult = Description.Create("Some description");
        DateTime start = new DateTime(2025, 3, 4, 12, 0, 0);
        DateTime end = new DateTime(2025, 3, 4, 13, 0, 0);
        VeaEvent = VeaEvent.Create(expectedTitleResult.payload, expectedDescriptionResult.payload, start, end).payload;
    }

    [Theory]
    [InlineData(5)]
    [InlineData(10)]
    [InlineData(25)]
    [InlineData(50)]
    public void UpdateEventMaximumNumberOfGuests_DraftState(int maxNoOfGuests)
    {
        // Arrange 
        var maxNoOfGuestsResult = MaxNoOfGuests.Create(maxNoOfGuests);

        // Act
        var updateMaxNoOfGuestsResult = VeaEvent.SetMaxNoOfGuests(maxNoOfGuestsResult.payload);

        // Assert
        Assert.True(maxNoOfGuestsResult.isSuccess);
        Assert.True(updateMaxNoOfGuestsResult.isSuccess);
        Assert.Equal(maxNoOfGuests, VeaEvent._maxNoOfGuests.Value);
    }

    [Theory]
    [InlineData(5)]
    [InlineData(10)]
    [InlineData(25)]
    [InlineData(50)]
    public void UpdateEventMaximumNumberOfGuests_ReadyState(int maxNoOfGuests)
    {
        // Arrange 
        VeaEvent.Readie();
        var maxNoOfGuestsResult = MaxNoOfGuests.Create(maxNoOfGuests);

        // Act
        var updateMaxNoOfGuestsResult = VeaEvent.SetMaxNoOfGuests(maxNoOfGuestsResult.payload);

        // Assert
        Assert.True(maxNoOfGuestsResult.isSuccess);
        Assert.True(updateMaxNoOfGuestsResult.isSuccess);
        Assert.Equal(maxNoOfGuests, VeaEvent._maxNoOfGuests.Value);
    }

    [Theory]
    [InlineData(5, 10)]
    [InlineData(10, 15)]
    [InlineData(25, 30)]
    [InlineData(45, 50)]
    [InlineData(20, 20)]
    [InlineData(20, 21)]
    public void UpdateEventMaximumNumberOfGuests_ActiveState(int maxNoOfGuestsPrev, int maxNoOfGuestsNew)
    {
        // Arrange 
        var maxNoOfGuestsPrevResult = MaxNoOfGuests.Create(maxNoOfGuestsPrev);
        VeaEvent.SetMaxNoOfGuests(maxNoOfGuestsPrevResult.payload);
        VeaEvent.Activate();
        var maxNoOfGuestsNewResult = MaxNoOfGuests.Create(maxNoOfGuestsNew);

        // Act
        var updateMaxNoOfGuestsResult = VeaEvent.SetMaxNoOfGuests(maxNoOfGuestsNewResult.payload);

        // Assert
        Assert.True(maxNoOfGuestsPrevResult.isSuccess);
        Assert.True(maxNoOfGuestsNewResult.isSuccess);
        Assert.True(updateMaxNoOfGuestsResult.isSuccess);
        Assert.Equal(maxNoOfGuestsNew, VeaEvent._maxNoOfGuests.Value);
    }

    [Theory]
    [InlineData(10, 5)]
    [InlineData(15, 5)]
    [InlineData(50, 45)]
    [InlineData(21, 20)]
    public void UpdateEventMaximumNumberOfGuests_ActiveState_ReducesNumberOfMaximumGuests(int maxNoOfGuestsPrev, int maxNoOfGuestsNew)
    {
        // Arrange 
        var maxNoOfGuestsPrevResult = MaxNoOfGuests.Create(maxNoOfGuestsPrev);
        VeaEvent.SetMaxNoOfGuests(maxNoOfGuestsPrevResult.payload);
        VeaEvent.Activate();
        var maxNoOfGuestsNewResult = MaxNoOfGuests.Create(maxNoOfGuestsNew);

        // Act
        var updateMaxNoOfGuestsResult = VeaEvent.SetMaxNoOfGuests(maxNoOfGuestsNewResult.payload);

        // Assert
        Assert.True(maxNoOfGuestsPrevResult.isSuccess);
        Assert.True(maxNoOfGuestsNewResult.isSuccess);
        Assert.True(updateMaxNoOfGuestsResult.isFailure);
        Assert.Equal(maxNoOfGuestsPrev, VeaEvent._maxNoOfGuests.Value);
        Assert.Contains(Error.CanNotReduceMaxNoOfGuestsOnActiveEvent(), updateMaxNoOfGuestsResult.errors);
    }

    [Theory]
    [InlineData(5)]
    [InlineData(10)]
    [InlineData(25)]
    [InlineData(50)]
    public void UpdateEventMaximumNumberOfGuests_CancelledState(int maxNoOfGuests)
    {
        // Arrange 
        var maxNoOfGuestsResult = MaxNoOfGuests.Create(maxNoOfGuests);
        VeaEvent.Cancel();

        // Act
        var updateMaxNoOfGuestsResult = VeaEvent.SetMaxNoOfGuests(maxNoOfGuestsResult.payload);

        // Assert
        Assert.True(maxNoOfGuestsResult.isSuccess);
        Assert.True(updateMaxNoOfGuestsResult.isFailure);
        Assert.Equal(5, VeaEvent._maxNoOfGuests.Value); // Test for default value
        Assert.Contains(Error.CanNotModifyCancelledEvent(), updateMaxNoOfGuestsResult.errors);
    }

    [Theory]
    [InlineData(4)]
    [InlineData(3)]
    public void UpdateEventMaximumNumberOfGuests_NumberTooSmall(int maxNoOfGuests)
    {
        // Arrange
        // None

        // Act
        var maxNoOfGuestsResult = MaxNoOfGuests.Create(maxNoOfGuests);

        // Assert
        Assert.True(maxNoOfGuestsResult.isFailure);
        Assert.Contains(Error.MaxNoOfGuestsTooSmall(), maxNoOfGuestsResult.errors);
    }

    [Theory]
    [InlineData(51)]
    [InlineData(52)]
    public void UpdateEventMaximumNumberOfGuests_NumberTooLarge(int maxNoOfGuests)
    {
        // Arrange
        // None

        // Act
        var maxNoOfGuestsResult = MaxNoOfGuests.Create(maxNoOfGuests);

        // Assert
        Assert.True(maxNoOfGuestsResult.isFailure);
        Assert.Contains(Error.MaxNoOfGuestsTooLarge(), maxNoOfGuestsResult.errors);
    }
}
