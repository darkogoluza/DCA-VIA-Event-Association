﻿using IntegrationTests.Common;
using Microsoft.Extensions.DependencyInjection;
using ViaEventAssociation.Core.Application.Common.CommandDispatcher;
using ViaEventAssociation.Core.Application.Features.Event.ActivateEvent;
using ViaEventAssociation.Core.Application.Features.Event.CreateEvent;
using ViaEventAssociation.Core.Application.Features.Event.ReadieEvent;
using ViaEventAssociation.Core.Application.Features.Event.UpdateDescription;
using ViaEventAssociation.Core.Application.Features.Event.UpdateMaxNoOfGuests;
using ViaEventAssociation.Core.Application.Features.Event.UpdateStartAndEndDateTime;
using ViaEventAssociation.Core.Application.Features.Event.UpdateTitle;
using ViaEventAssociation.Core.Application.Features.Event.UpdateVisibility;
using ViaEventAssociation.Core.Application.Features.Guest.RegisterGuest;
using ViaEventAssociation.Core.Application.Features.Invitation.AcceptInvitation;
using ViaEventAssociation.Core.Application.Features.Invitation.Invate;
using VIAEventAssociation.Core.Domain.Common.Repositories;
using VIAEventAssociation.Core.Domain.Common.Values;
using ViaEventAssociation.Infrastructure.SqliteDmPersistence;

namespace IntegrationTests.Repositories.Invitation;

public class AcceptInvitationIntegrationTest : IDisposable
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly ServiceProvider _serviceProvider;

    public AcceptInvitationIntegrationTest()
    {
        _serviceProvider = TestServiceProvider.CreateServiceProvider();

        _commandDispatcher = _serviceProvider.GetRequiredService<ICommandDispatcher>();
    }

    public void Dispose()
    {
        _serviceProvider.GetRequiredService<SqliteDmContext>().ChangeTracker.Clear();
    }

    [Fact]
    public async Task GivenGuestIsInvited_WhenTheyAccept_ThenInvitationIsMarkedAsAccepted()
    {
        // Arrange

        //Event
        var repoEvent = _serviceProvider.GetRequiredService<IEventRepository>();
        var commandCreate = CreateEventCommand.Create().payload;
        await _commandDispatcher.DispatchAsync(commandCreate);

        var commandUpdateTitle =
            UpdateEventTitleCommand.Create(commandCreate.VeaEvent.VeaEventId.Id, "New Title").payload;
        var commandUpdateDescription = UpdateEventDescriptionCommand
            .Create(commandCreate.VeaEvent.VeaEventId.Id, "New Description").payload;
        var commandUpdateMaxNoOfGuests =
            UpdateEventMaxNoOfGuestsCommand.Create(commandCreate.VeaEvent.VeaEventId.Id, 10).payload;
        var commandUpdateStartEndDateTime = UpdateEventStartAndEndDateTimeCommand
            .Create(commandCreate.VeaEvent.VeaEventId.Id, new DateTime(2025, 3, 4, 12, 0, 0),
                new DateTime(2025, 3, 4, 13, 0, 0)).payload;
        var commandUpdateVisibility =
            UpdateEventVisibilityCommand.Create(commandCreate.VeaEvent.VeaEventId.Id, true).payload;
        DateTime CurrentDateTimeMock() => new DateTime(2025, 3, 3, 12, 0, 0);
        var commandReadieEvent =
            ReadieEventCommand.Create(commandCreate.VeaEvent.VeaEventId.Id, CurrentDateTimeMock).payload;
        var commandActiveEvent =
            ActivateEventCommand.Create(commandCreate.VeaEvent.VeaEventId.Id).payload;

        await _commandDispatcher.DispatchAsync(commandUpdateTitle);
        await _commandDispatcher.DispatchAsync(commandUpdateDescription);
        await _commandDispatcher.DispatchAsync(commandUpdateMaxNoOfGuests);
        await _commandDispatcher.DispatchAsync(commandUpdateStartEndDateTime);
        await _commandDispatcher.DispatchAsync(commandUpdateVisibility);
        await _commandDispatcher.DispatchAsync(commandReadieEvent);
        await _commandDispatcher.DispatchAsync(commandActiveEvent);

        //Guest
        string firstName = "John";
        string lastName = "Doe";
        string email = "jhd@via.dk";
        string url =
            "https://media.istockphoto.com/id/521573873/vector/unknown-person-silhouette-whith-blue-tie.jpg?s=2048x2048&w=is&k=20&c=cjOrS4d7gV46uXDx9iWH5n5uSEF6hhZ6Gebbp5j6USI=";

        var commandRegister = RegisterGuestCommand.Create(firstName, lastName, email, url)
            .payload;

        await _commandDispatcher.DispatchAsync(commandRegister);

        // Invitation
        var commandInvite =
            GuestInvitedCommand.Create(commandCreate.VeaEvent.VeaEventId.Id, commandRegister.Guest.GuestId.Id).payload;
        await _commandDispatcher.DispatchAsync(commandInvite);

        var commandAcceptInvite = GuestAcceptsInvitationCommand
            .Create(commandCreate.VeaEvent.VeaEventId.Id, commandRegister.Guest.GuestId.Id, CurrentDateTimeMock).payload;

        // Act
        var result = await _commandDispatcher.DispatchAsync(commandAcceptInvite);

        // Assert
        Assert.True(result.isSuccess);
        Assert.Single((await repoEvent.GetAsync(commandCreate.VeaEvent.VeaEventId.Id))._invitations);
        Assert.Equal(StatusType.Accepted,
            (await repoEvent.GetAsync(commandCreate.VeaEvent.VeaEventId.Id))._invitations.First()._statusType);
    }
}
