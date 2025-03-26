using ViaEventAssociation.Core.Application.Features.Event.GuestParticipateEvent;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Values;
using VIAEventAssociation.Core.Domain.Aggregates.Guests.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Features.Invitation;

public class GuestInvitedCommand
{
    public VeaEventId VeaEventId { get; private set; }
    public GuestId GuestId { get; private set; }

    public static Result<GuestInvitedCommand> Create(Guid eventId, Guid guestId)
    {
        var veaEventIdResult = VeaEventId.Create(eventId);
        var guestIdResult = GuestId.Create(guestId);
        GuestInvitedCommand command =
            new GuestInvitedCommand(veaEventIdResult.payload, guestIdResult.payload);
        Result<GuestInvitedCommand> result = Result<GuestInvitedCommand>
            .FromResult(veaEventIdResult)
            .WithResult(guestIdResult);

        if (result.isSuccess)
        {
            result.payload = command;
        }

        return result;
    }

    private GuestInvitedCommand(VeaEventId veaEventId, GuestId guestId)
    {
        VeaEventId = veaEventId;
        GuestId = guestId;
    }
}
