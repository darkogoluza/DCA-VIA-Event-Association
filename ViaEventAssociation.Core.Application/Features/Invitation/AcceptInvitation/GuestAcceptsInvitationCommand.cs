using VIAEventAssociation.Core.Domain.Aggregates.Events.Values;
using VIAEventAssociation.Core.Domain.Aggregates.Guests.Values;
using VIAEventAssociation.Core.Domain.Contracts;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Features.Invitation.AcceptInvitation;

public class GuestAcceptsInvitationCommand
{
    public VeaEventId VeaEventId { get; private set; }
    public GuestId GuestId { get; private set; }
    public CurrentDateTime? CurrentDateTime;

    public static Result<GuestAcceptsInvitationCommand> Create(Guid eventId, Guid guestId, CurrentDateTime? currentDateTime = null)
    {
        var veaEventIdResult = VeaEventId.Create(eventId);
        var guestIdResult = GuestId.Create(guestId);
        GuestAcceptsInvitationCommand command =
            new GuestAcceptsInvitationCommand(veaEventIdResult.payload, guestIdResult.payload, currentDateTime);
        Result<GuestAcceptsInvitationCommand> result = Result<GuestAcceptsInvitationCommand>
            .FromResult(veaEventIdResult)
            .WithResult(guestIdResult);

        if (result.isSuccess)
        {
            result.payload = command;
        }

        return result;
    }

    private GuestAcceptsInvitationCommand(VeaEventId veaEventId, GuestId guestId, CurrentDateTime? currentDateTime)
    {
        VeaEventId = veaEventId;
        GuestId = guestId;
        CurrentDateTime = currentDateTime;
    }
}
