using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Values;
using VIAEventAssociation.Core.Domain.Aggregates.Guests.Entities;
using VIAEventAssociation.Core.Domain.Aggregates.Guests.Values;
using VIAEventAssociation.Core.Domain.Aggregates.Invitations.Entities;
using VIAEventAssociation.Core.Domain.Common.Values;
using VIAEventAssociation.Core.Domain.Services;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.GuestTests.GuestInvite;

public class GuestInvitedUnitTests
{
    private readonly VeaEvent VeaEvent;
    private readonly Guest Guest;

    private DateTime CurrentDateTimeMock() => new DateTime(2025, 3, 3, 12, 0, 0);

    private readonly GuestInviteService _guestInviteService;

    public GuestInvitedUnitTests()
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
        VeaEvent._visibility = true;
        VeaEvent._maxNoOfGuests = expectedMaxNoOfGuestsResult.payload;
        Guest = Guest.Create(firstNameResult.payload, lastNameResult.payload, emailResult.payload, profilePictureUrl)
            .payload;

        // Assert
        Assert.NotEmpty(VeaEvent.VeaEventId.Id.ToString());
        Assert.Equal(EventStatusType.Draft, VeaEvent._eventStatusType);
        Assert.NotEmpty(Guest.GuestId.Id.ToString());
    }

    [Fact]
    public void GuestInvited()
    {
        // Arrange
        VeaEvent.Readie(CurrentDateTimeMock);
        VeaEvent.Activate();

        // Act
        var inviteGuestResult = _guestInviteService.InviteGuest(Guest.GuestId, VeaEvent);

        // Assert
        Assert.True(inviteGuestResult.isSuccess);
        Assert.Single(VeaEvent._invitations);
    }

    [Fact]
    public void GuestInvited_DraftState()
    {
        // Arrange

        // Act
        var inviteGuestResult = _guestInviteService.InviteGuest(Guest.GuestId, VeaEvent);

        // Assert
        Assert.True(inviteGuestResult.isFailure);
        Assert.Contains(Error.CanNotInviteToDraftEvent(), inviteGuestResult.errors);
    }

    [Fact]
    public void GuestInvited_CancelledState()
    {
        // Arrange
        VeaEvent.Cancel();

        // Act
        var inviteGuestResult = _guestInviteService.InviteGuest(Guest.GuestId, VeaEvent);

        // Assert
        Assert.True(inviteGuestResult.isFailure);
        Assert.Contains(Error.CanNotInviteToCancelledEvent(), inviteGuestResult.errors);
    }

    [Fact]
    public void GuestInvited_NoMoreRoom_1()
    {
        // Arrange
        VeaEvent.Readie(CurrentDateTimeMock);
        VeaEvent.Activate();
        VeaEvent._guests.Add(Guest);
        VeaEvent._guests.Add(Guest);
        VeaEvent._guests.Add(Guest);
        VeaEvent._guests.Add(Guest);
        VeaEvent._guests.Add(Guest);

        // Act
        var inviteGuestResult = _guestInviteService.InviteGuest(Guest.GuestId, VeaEvent);

        // Assert
        Assert.True(inviteGuestResult.isFailure);
        Assert.Contains(Error.CanNotInviteEventIsFull(), inviteGuestResult.errors);
    }

    [Fact]
    public void GuestInvited_NoMoreRoom_2()
    {
        // Arrange
        VeaEvent.Readie(CurrentDateTimeMock);
        VeaEvent.Activate();
        VeaEvent._guests.Add(Guest);
        VeaEvent._guests.Add(Guest);
        VeaEvent._guests.Add(Guest);
        VeaEvent._guests.Add(Guest);
        VeaEvent._guests.Add(Guest);
        VeaEvent._guests.Add(Guest);

        // Act
        var inviteGuestResult = _guestInviteService.InviteGuest(Guest.GuestId, VeaEvent);

        // Assert
        Assert.True(inviteGuestResult.isFailure);
        Assert.Contains(Error.CanNotInviteEventIsFull(), inviteGuestResult.errors);
    }

    [Fact]
    public void GuestInvited_NoMoreRoom_3()
    {
        // Arrange
        var invitation = Invitation.Create(StatusType.Pending, Guest.GuestId).payload;

        VeaEvent.Readie(CurrentDateTimeMock);
        VeaEvent.Activate();
        VeaEvent._guests.Add(Guest);
        VeaEvent._guests.Add(Guest);
        VeaEvent._guests.Add(Guest);
        VeaEvent._guests.Add(Guest);
        VeaEvent._invitations.Add(invitation);

        // Act
        var inviteGuestResult = _guestInviteService.InviteGuest(Guest.GuestId, VeaEvent);

        // Assert
        Assert.True(inviteGuestResult.isFailure);
        Assert.Contains(Error.CanNotInviteEventIsFull(), inviteGuestResult.errors);
    }

    [Fact]
    public void GuestInvited_GuestAlreadyParticipating()
    {
        // Arrange
        VeaEvent.Readie(CurrentDateTimeMock);
        VeaEvent.Activate();
        VeaEvent._guests.Add(Guest);

        // Act
        var inviteGuestResult = _guestInviteService.InviteGuest(Guest.GuestId, VeaEvent);

        // Assert
        Assert.True(inviteGuestResult.isFailure);
        Assert.Contains(Error.GuestIsAlreadyParticipating(), inviteGuestResult.errors);
    }

    [Fact]
    public void GuestInvited_GuestAlreadyInvited()
    {
        // Arrange
        VeaEvent.Readie(CurrentDateTimeMock);
        VeaEvent.Activate();

        // Act
        _guestInviteService.InviteGuest(Guest.GuestId, VeaEvent);
        var inviteGuestResult = _guestInviteService.InviteGuest(Guest.GuestId, VeaEvent);

        // Assert
        Assert.True(inviteGuestResult.isFailure);
        Assert.Contains(Error.GuestAlreadyInvited(), inviteGuestResult.errors);
    }
}
