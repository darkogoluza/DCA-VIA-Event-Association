using VIAEventAssociation.Core.Domain.Aggregates.Events.Values;
using VIAEventAssociation.Core.Domain.Aggregates.Guests.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Features.Invitation.DeclineInvitation;

public class GuestDeclineInvitationCommand
{
    public VeaEventId VeaEventId { get; private set; }
    public GuestId GuestId { get; private set; }

    public static Result<GuestDeclineInvitationCommand> Create(Guid eventId, Guid guestId)
    {
        var veaEventIdResult = VeaEventId.Create(eventId);
        var guestIdResult = GuestId.Create(guestId);
        GuestDeclineInvitationCommand command =
            new GuestDeclineInvitationCommand(veaEventIdResult.payload, guestIdResult.payload);
        Result<GuestDeclineInvitationCommand> result = Result<GuestDeclineInvitationCommand>
            .FromResult(veaEventIdResult)
            .WithResult(guestIdResult);

        if (result.isSuccess)
        {
            result.payload = command;
        }

        return result;
    }

    private GuestDeclineInvitationCommand(VeaEventId veaEventId, GuestId guestId)
    {
        VeaEventId = veaEventId;
        GuestId = guestId;
    }
}
