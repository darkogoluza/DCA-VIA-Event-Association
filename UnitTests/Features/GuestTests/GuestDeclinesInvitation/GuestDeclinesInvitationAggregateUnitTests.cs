using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Values;
using VIAEventAssociation.Core.Domain.Aggregates.Guests.Entities;
using VIAEventAssociation.Core.Domain.Aggregates.Guests.Values;
using VIAEventAssociation.Core.Domain.Aggregates.Invitations.Entities;
using VIAEventAssociation.Core.Domain.Common.Values;
using VIAEventAssociation.Core.Domain.Services;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.GuestTests.GuestDeclinesInvitation;

public class GuestDeclinesInvitationAggregateUnitTests
{
    private readonly VeaEvent VeaEvent;
    private readonly Guest Guest;

    private DateTime CurrentDateTimeMock() => new DateTime(2025, 3, 3, 12, 0, 0);

    private readonly GuestInviteService _guestInviteService;

    public GuestDeclinesInvitationAggregateUnitTests()
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
        VeaEvent.Readie(CurrentDateTimeMock);
        _guestInviteService.InviteGuest(Guest.GuestId, VeaEvent);

        // Assert
        Assert.NotEmpty(VeaEvent.VeaEventId.Id.ToString());
        Assert.Equal(EventStatusType.Ready, VeaEvent._eventStatusType);
        Assert.NotEmpty(Guest.GuestId.Id.ToString());
        Assert.Single(VeaEvent._invitations);
        Assert.Equal(StatusType.Pending, VeaEvent._invitations[0]._statusType);
    }

    [Fact]
    public void GuestDeclinesInvitation_PendingState()
    {
        // Arrange
        VeaEvent.Activate();

        // Act
        var declineInvitationResult = VeaEvent.DeclineInvitation(Guest);

        // Assert
        Assert.True(declineInvitationResult.isSuccess);
        Assert.Equal(StatusType.Rejected, VeaEvent._invitations[0]._statusType);
    }

    [Fact]
    public void GuestDeclinesInvitation_AcceptedState()
    {
        // Arrange
        VeaEvent.Activate();
        VeaEvent.AcceptInvitation(Guest);

        // Act
        var declineInvitationResult = VeaEvent.DeclineInvitation(Guest);

        // Assert
        Assert.True(declineInvitationResult.isSuccess);
        Assert.Equal(StatusType.Rejected, VeaEvent._invitations[0]._statusType);
    }

    [Fact]
    public void GuestDeclinesInvitation_InvitationNotFound()
    {
        // Arrange
        VeaEvent.Activate();
        VeaEvent._invitations = new List<Invitation>(); // Delete all previous invitations

        // Act
        var declineInvitationResult = VeaEvent.DeclineInvitation(Guest);

        // Assert
        Assert.True(declineInvitationResult.isFailure);
        Assert.Contains(Error.InvitationNotFound(), declineInvitationResult.errors);
    }

    [Fact]
    public void GuestDeclinesInvitation_EventIsCancelled()
    {
        // Arrange
        VeaEvent.Cancel();

        // Act
        var declineInvitationResult = VeaEvent.DeclineInvitation(Guest);

        // Assert
        Assert.True(declineInvitationResult.isFailure);
        Assert.Contains(Error.CanNotDeclineInvitationOnCancelledEvent(), declineInvitationResult.errors);
    }
}
