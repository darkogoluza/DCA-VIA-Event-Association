using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Values;
using VIAEventAssociation.Core.Domain.Aggregates.Guests.Entities;
using VIAEventAssociation.Core.Domain.Aggregates.Guests.Values;
using VIAEventAssociation.Core.Domain.Aggregates.Invitations.Entities;
using VIAEventAssociation.Core.Domain.Common.Values;
using VIAEventAssociation.Core.Domain.Services;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.GuestTests.GuestParticipatesPublicEvent;

public class GuestParticipatesPublicEventUnitTests
{
    private readonly VeaEvent VeaEvent;
    private readonly Guest Guest;
    private readonly GuestInviteService _guestInviteService;

    private DateTime CurrentDateTimeMock() => new DateTime(2025, 3, 3, 12, 0, 0);

    public GuestParticipatesPublicEventUnitTests()
    {
        _guestInviteService = new GuestInviteService();

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
        VeaEvent._visibility = false;
        VeaEvent._maxNoOfGuests = expectedMaxNoOfGuestsResult.payload;
        Guest = Guest.Create(firstNameResult.payload, lastNameResult.payload, emailResult.payload,
            profilePictureUrl).payload;

        // Assert
        Assert.NotEmpty(VeaEvent.VeaEventId.Id.ToString());
        Assert.Equal(EventStatusType.Draft, VeaEvent._eventStatusType);
        Assert.NotEmpty(Guest.GuestId.Id.ToString());
    }

    [Fact]
    public void GuestParticipatesPublicEvent()
    {
        // Arrange
        VeaEvent.Readie(CurrentDateTimeMock);
        VeaEvent.Activate();
        VeaEvent.SetVisibility(true);

        // Act
        var participateInEventResult = VeaEvent.Participate(Guest);

        // Assert
        Assert.True(participateInEventResult.isSuccess);
        Assert.Contains(Guest, VeaEvent._guests);
    }

    [Fact]
    public void GuestParticipatesPublicEvent_DraftState()
    {
        // Arrange
        VeaEvent.SetVisibility(true);

        // Act
        var participateInEventResult = VeaEvent.Participate(Guest);

        // Assert
        Assert.True(participateInEventResult.isFailure);
        Assert.Contains(Error.CanNotJoinDraftEvent(), participateInEventResult.errors);
    }

    [Fact]
    public void GuestParticipatesPublicEvent_CancelledState()
    {
        // Arrange
        VeaEvent.SetVisibility(true);
        VeaEvent.Cancel();

        // Act
        var participateInEventResult = VeaEvent.Participate(Guest);

        // Assert
        Assert.True(participateInEventResult.isFailure);
        Assert.Contains(Error.CanNotJoinCancelledEvent(), participateInEventResult.errors);
    }

    [Fact]
    public void GuestParticipatesPublicEvent_NoMoreRoom_1()
    {
        // Arrange
        VeaEvent.Readie(CurrentDateTimeMock);
        VeaEvent.Activate();
        VeaEvent.SetVisibility(true);
        VeaEvent._guests.Add(Guest);
        VeaEvent._guests.Add(Guest);
        VeaEvent._guests.Add(Guest);
        VeaEvent._guests.Add(Guest);
        VeaEvent._guests.Add(Guest);

        // Act
        var participateInEventResult = VeaEvent.Participate(Guest);

        // Assert
        Assert.True(participateInEventResult.isFailure);
        Assert.Contains(Error.CanNotJoinEventIsFull(), participateInEventResult.errors);
    }

    [Fact]
    public void GuestParticipatesPublicEvent_NoMoreRoom_2()
    {
        // Arrange
        VeaEvent.Readie(CurrentDateTimeMock);
        VeaEvent.Activate();
        VeaEvent.SetVisibility(true);
        VeaEvent._guests.Add(Guest);
        VeaEvent._guests.Add(Guest);
        VeaEvent._guests.Add(Guest);
        VeaEvent._guests.Add(Guest);
        VeaEvent._guests.Add(Guest);
        VeaEvent._guests.Add(Guest);

        // Act
        var participateInEventResult = VeaEvent.Participate(Guest);

        // Assert
        Assert.True(participateInEventResult.isFailure);
        Assert.Contains(Error.CanNotJoinEventIsFull(), participateInEventResult.errors);
    }

    [Fact]
    public void GuestParticipatesPublicEvent_NoMoreRoom_3()
    {
        // Arrange
        var invitation = Invitation.Create(StatusType.Pending, Guest.GuestId).payload;

        VeaEvent.Readie(CurrentDateTimeMock);
        VeaEvent.Activate();
        VeaEvent.SetVisibility(true);
        VeaEvent._guests.Add(Guest);
        VeaEvent._guests.Add(Guest);
        VeaEvent._guests.Add(Guest);
        VeaEvent._guests.Add(Guest);
        VeaEvent._invitations.Add(invitation);

        // Act
        var participateInEventResult = VeaEvent.Participate(Guest);

        // Assert
        Assert.True(participateInEventResult.isFailure);
        Assert.Contains(Error.CanNotJoinEventIsFull(), participateInEventResult.errors);
    }

    [Fact]
    public void GuestParticipatesPublicEvent_GuestAlreadyParticipating()
    {
        // Arrange
        VeaEvent.Readie(CurrentDateTimeMock);
        VeaEvent.Activate();
        VeaEvent.SetVisibility(true);

        // Act
        VeaEvent.Participate(Guest);
        var participateInEventResult = VeaEvent.Participate(Guest);

        // Assert
        Assert.True(participateInEventResult.isFailure);
        Assert.Contains(Error.GuestIsAlreadyParticipating(), participateInEventResult.errors);
    }

    [Fact]
    public void GuestParticipatesPublicEvent_CannotJoinPrivateEvent()
    {
        // Arrange
        VeaEvent.Readie(CurrentDateTimeMock);
        VeaEvent.Activate();
        VeaEvent.SetVisibility(false);

        // Act
        var participateInEventResult = VeaEvent.Participate(Guest);

        // Assert
        Assert.True(participateInEventResult.isFailure);
        Assert.Contains(Error.CanNotJoinPrivateEvent(), participateInEventResult.errors);
    }
}
