using UnitTests.Fakes;
using UnitTests.Stubs;
using ViaEventAssociation.Core.Application.Common.CommandHandler;
using ViaEventAssociation.Core.Application.Features.Event.CreateEvent;
using ViaEventAssociation.Core.Application.Features.Event.GuestParticipateEvent;
using ViaEventAssociation.Core.Application.Features.Invitation;
using ViaEventAssociation.Core.Application.Features.Invitation.Invate;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Values;
using VIAEventAssociation.Core.Domain.Aggregates.Guests.Entities;
using VIAEventAssociation.Core.Domain.Aggregates.Guests.Values;
using VIAEventAssociation.Core.Domain.Common.Repositories;
using VIAEventAssociation.Core.Domain.Common.Values;

namespace UnitTests.Features.GuestTests.GuestInvite;

public class GuestInvitedCommandHandlerTest
{
    private readonly InMemEventRepoStub repoEvent = new();
    private VeaEvent _veaEvent;
    private Guest _guest;

    private DateTime CurrentDateTimeMock() => new DateTime(2025, 3, 3, 12, 0, 0);

    public GuestInvitedCommandHandlerTest()
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
        ICommandHandler<CreateEventCommand> handlerEvent = new CreateEventHandler(repoEvent);

        CreateEventCommand commandEvent = CreateEventCommand.Create().payload;
        handlerEvent.HandleAsync(commandEvent);
        _veaEvent = repoEvent.Events[0];

        _veaEvent._title = expectedTitleResult.payload;
        _veaEvent._description = expectedDescriptionResult.payload;
        _veaEvent._startDateTime = new DateTime(2025, 3, 4, 12, 0, 0);
        _veaEvent._endDateTime = new DateTime(2025, 3, 4, 13, 0, 0);
        _veaEvent._visibility = true;
        _veaEvent._maxNoOfGuests = expectedMaxNoOfGuestsResult.payload;
        _veaEvent.Readie(CurrentDateTimeMock);
        _veaEvent.Activate();
        _guest = Guest.Create(firstNameResult.payload, lastNameResult.payload, emailResult.payload, profilePictureUrl)
            .payload;
    }

    [Fact]
    public async Task GuestInvited()
    {
        // Arrange
        ICommandHandler<GuestInvitedCommand> handler = new GuestInvitationHandler(repoEvent);

        GuestInvitedCommand command =
            GuestInvitedCommand.Create(_veaEvent.VeaEventId.Id, _guest.GuestId.Id).payload;

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        Assert.True(result.isSuccess);
        Assert.Single(repoEvent.Events);
        Assert.Single(_veaEvent._invitations);
    }
}
