using UnitTests.Fakes;
using UnitTests.Stubs;
using ViaEventAssociation.Core.Application.Common.CommandHandler;
using ViaEventAssociation.Core.Application.Features.Event.CreateEvent;
using ViaEventAssociation.Core.Application.Features.Guest.RegisterGuest;
using ViaEventAssociation.Core.Application.Features.Invitation.AcceptInvitation;
using ViaEventAssociation.Core.Application.Features.Invitation.Invate;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Values;
using VIAEventAssociation.Core.Domain.Aggregates.Guests.Entities;
using VIAEventAssociation.Core.Domain.Common.Repositories;
using VIAEventAssociation.Core.Domain.Common.Values;

namespace UnitTests.Features.GuestTests.GuestAcceptsInvitation;

public class GuestAcceptInvitationCommandHandlerTest
{
    private readonly InMemEventRepoStub repoEvent = new();
    private readonly InMemGuestRepoStub repoGuest = new();
    private VeaEvent _veaEvent;
    private Guest _guest;

    private DateTime CurrentDateTimeMock() => new DateTime(2025, 3, 3, 12, 0, 0);

    public GuestAcceptInvitationCommandHandlerTest()
    {
        // Arrange 
        var expectedTitleResult = Title.Create("Working Title");
        var expectedDescriptionResult = Description.Create("Some description");
        var expectedMaxNoOfGuestsResult = MaxNoOfGuests.Create(5);

        // Act
        IUnitOfWork uow = new FakeUoW();
        ICommandHandler<CreateEventCommand> handlerEvent = new CreateEventHandler(repoEvent, uow);

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

        ICommandHandler<RegisterGuestCommand> handlerGuest = new RegisterGuestHandler(repoGuest, uow);
        RegisterGuestCommand commandGuest = RegisterGuestCommand.Create("John", "Doe", "jhd@via.dk",
                "https://media.istockphoto.com/id/521573873/vector/unknown-person-silhouette-whith-blue-tie.jpg?s=2048x2048&w=is&k=20&c=cjOrS4d7gV46uXDx9iWH5n5uSEF6hhZ6Gebbp5j6USI=")
            .payload;
        handlerGuest.HandleAsync(commandGuest);
        _guest = repoGuest.Guests[0];

        ICommandHandler<GuestInvitedCommand> handlerInvite = new GuestInvitationHandler(repoEvent, uow);

        GuestInvitedCommand commandInvite =
            GuestInvitedCommand.Create(_veaEvent.VeaEventId.Id, _guest.GuestId.Id).payload;

        handlerInvite.HandleAsync(commandInvite);
    }

    [Fact]
    public async Task GuestAcceptInvited()
    {
        // Arrange
        IUnitOfWork uow = new FakeUoW();
        ICommandHandler<GuestAcceptsInvitationCommand> handler = new GuestAcceptsInvitationHandler(repoEvent, repoGuest, uow);

        GuestAcceptsInvitationCommand command =
            GuestAcceptsInvitationCommand.Create(_veaEvent.VeaEventId.Id, _guest.GuestId.Id, CurrentDateTimeMock).payload;

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        Assert.True(result.isSuccess);
        Assert.Single(repoEvent.Events);
        Assert.Single(_veaEvent._invitations);
        Assert.Equal(StatusType.Accepted, _veaEvent._invitations[0]._statusType);
    }
}
