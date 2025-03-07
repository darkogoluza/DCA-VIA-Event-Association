using VIAEventAssociation.Core.Domain.Aggregates.Events.Values;
using VIAEventAssociation.Core.Domain.Aggregates.Guests.Entities;
using VIAEventAssociation.Core.Domain.Aggregates.Guests.Values;
using VIAEventAssociation.Core.Domain.Common.Bases;
using VIAEventAssociation.Core.Domain.Common.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace VIAEventAssociation.Core.Domain.Aggregates.Invitations.Entities;

public class Invitation : AggregateRoot
{
    private StatusType _statusType;
    private GuestId _inviteeId;

    private Invitation(StatusType statusType, GuestId inviteeId)
    {
        _statusType = statusType;
        _inviteeId = inviteeId;
    }

    public static Result<Invitation> Create(StatusType statusType, GuestId inviteeId)
    {
        throw new NotImplementedException();
    }
}
