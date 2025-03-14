using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;
using VIAEventAssociation.Core.Domain.Aggregates.Guests.Values;
using VIAEventAssociation.Core.Domain.Aggregates.Invitations.Entities;
using VIAEventAssociation.Core.Domain.Common.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace VIAEventAssociation.Core.Domain.Services;

public class GuestInviteService
{
    public Result<None> InviteGuest(GuestId guestId, VeaEvent veaEvent)
    {
        var result = Invitation.Create(StatusType.Pending, guestId);

        if (result.isFailure)
            return result.errors.ToArray();

        var inviteResult = veaEvent.Invite(result.payload);

        if (inviteResult.isFailure)
            return inviteResult.errors.ToArray();

        return Result<None>.Success();
    }
}
