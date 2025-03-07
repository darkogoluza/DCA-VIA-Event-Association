using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Values;
using VIAEventAssociation.Core.Domain.Aggregates.Guests.Entities;
using VIAEventAssociation.Core.Domain.Aggregates.Guests.Values;
using VIAEventAssociation.Core.Domain.Common.Bases;
using VIAEventAssociation.Core.Domain.Common.Values;
using ViaEventAssociation.Core.Tools.OperationResult;


public class RequestToJoin : AggregateRoot
{
    private StatusType _statusType;
    private Reasion _reason;
    private GuestId _invitorId;
    private VeaEventId _veaEventId;

    private RequestToJoin(StatusType statusType, Reasion reason, GuestId invitorId, VeaEventId veaEventId)
    {
        _statusType = statusType;
        _reason = reason;
        _invitorId = invitorId;
        _veaEventId = veaEventId;
    }

    public static Result<RequestToJoin> Create(StatusType statusType, Reasion reason, GuestId invitorId, VeaEventId veaEventId)
    {
        throw new NotImplementedException();
    }

    public Result<None> ApproveJoinRequest()
    {
        throw new NotImplementedException();
    }

    public Result<None> DeclineJoinRequest()
    {
        throw new NotImplementedException();
    }
}
