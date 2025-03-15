using VIAEventAssociation.Core.Domain.Aggregates.Guests.Values;
using VIAEventAssociation.Core.Domain.Aggregates.Invitations.Values;
using VIAEventAssociation.Core.Domain.Common.Bases;
using VIAEventAssociation.Core.Domain.Common.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace VIAEventAssociation.Core.Domain.Aggregates.Invitations.Entities;

public class Invitation : AggregateRoot
{
    public InvitationId InvitationId;
    internal StatusType _statusType;
    internal GuestId _inviteeId;

    private Invitation(InvitationId id, StatusType statusType, GuestId inviteeId) : base(id.Id)
    {
        _statusType = statusType;
        _inviteeId = inviteeId;
        InvitationId = id;
    }

    public static Result<Invitation> Create(StatusType statusType, GuestId inviteeId)
    {
        var invitationIdResult = InvitationId.Create(Guid.NewGuid());

        return new Invitation(invitationIdResult.payload, statusType, inviteeId);
    }
}
