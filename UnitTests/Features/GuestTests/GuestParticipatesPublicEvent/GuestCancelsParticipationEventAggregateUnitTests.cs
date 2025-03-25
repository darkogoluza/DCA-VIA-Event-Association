using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Values;
using VIAEventAssociation.Core.Domain.Aggregates.Guests.Entities;
using VIAEventAssociation.Core.Domain.Aggregates.Guests.Values;
using VIAEventAssociation.Core.Domain.Common.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.GuestTests.GuestParticipatesPublicEvent;

public class GuestCancelsParticipationEventAggregateUnitTests
{
    private readonly VeaEvent VeaEvent;
    private readonly Guest Guest;

    private DateTime CurrentDateTimeMock() => new DateTime(2025, 3, 3, 12, 0, 0);
    private DateTime CurrentDateTimeMockInTheFuture() => new DateTime(2025, 3, 5, 12, 0, 0);

    public GuestCancelsParticipationEventAggregateUnitTests()
    {
        // Arrange
        var expectedTitleResult = Title.Create("Working Title");
        var expectedDescriptionResult = Description.Create("Some description");
        var expectedMaxNoOfGuestsResult = MaxNoOfGuests.Create(5);
        var firstNameResult = FirstName.Create("John");
        var lastNameResult = LastName.Create("Doe");
        var emailResult = Email.Create("jdo@via.dk");
        Uri profilePictureUrl =
            new Uri(
                "https://media.istockphoto.com/id/521573873/vector/unknown-person-silhouette-whith-blue-tie.jpg?s=2048x2048&w=is&k=20&c=cjOrS4d7gV46uXDx9iWH5n5uSEF6hhZ6Gebbp5j6USI=");

        // Act
        VeaEvent = VeaEvent.Create().payload;
        VeaEvent._title = expectedTitleResult.payload;
        VeaEvent._description = expectedDescriptionResult.payload;
        VeaEvent._startDateTime = new DateTime(2025, 3, 4, 12, 0, 0);
        VeaEvent._endDateTime = new DateTime(2025, 3, 4, 13, 0, 0);
        VeaEvent._visibility = true;
        VeaEvent._maxNoOfGuests = expectedMaxNoOfGuestsResult.payload;
        Guest = Guest.Create(firstNameResult.payload, lastNameResult.payload, emailResult.payload, profilePictureUrl).payload;
        VeaEvent.Readie(CurrentDateTimeMock);
        VeaEvent.Activate();
        VeaEvent.Participate(Guest);

        // Assert
        Assert.NotEmpty(VeaEvent.VeaEventId.Id.ToString());
        Assert.Equal(EventStatusType.Active, VeaEvent._eventStatusType);
        Assert.NotEmpty(Guest.GuestId.Id.ToString());
        Assert.Contains(Guest, VeaEvent._guests);
    }

    [Fact]
    public void GuestCancelsParticipatesPublicEvent()
    {
        // Arrange 

        // Act
        var cancelsParticipateResult = VeaEvent.CancelsParticipate(Guest.GuestId, CurrentDateTimeMock);

        // Assert
        Assert.True(cancelsParticipateResult.isSuccess);
        Assert.DoesNotContain(Guest, VeaEvent._guests);
    }

    [Fact]
    public void GuestCancelsParticipatesPublicEvent_Twice()
    {
        // Arrange 
        VeaEvent.CancelsParticipate(Guest.GuestId);

        // Act
        var cancelsParticipateResult = VeaEvent.CancelsParticipate(Guest.GuestId, CurrentDateTimeMock);

        // Assert
        Assert.True(cancelsParticipateResult.isSuccess);
        Assert.DoesNotContain(Guest, VeaEvent._guests);
    }

    [Fact]
    public void GuestCancelsParticipatesPublicEvent_EventIsInPast()
    {
        // Arrange 

        // Act
        var cancelsParticipateResult = VeaEvent.CancelsParticipate(Guest.GuestId, CurrentDateTimeMockInTheFuture);

        // Assert
        Assert.True(cancelsParticipateResult.isFailure);
        Assert.Contains(Error.EventIsInPast(), cancelsParticipateResult.errors);
    }
}
