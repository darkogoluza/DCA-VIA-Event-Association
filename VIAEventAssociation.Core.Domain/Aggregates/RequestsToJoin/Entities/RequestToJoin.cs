using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Values;
using VIAEventAssociation.Core.Domain.Aggregates.Guests.Entities;
using VIAEventAssociation.Core.Domain.Common.Bases;
using VIAEventAssociation.Core.Domain.Common.Values;
using ViaEventAssociation.Core.Tools.OperationResult;


public class RequestToJoin : AggregateRoot
{
    private StatusType _statusType;
    private Reasion _reason ;
    private Guest _invitor;
    private VeaEvent _veaEvent;

    private RequestToJoin(StatusType statusType, Reasion reason , Guest invitor, VeaEvent veaEvent)
    {
        _statusType = statusType;
        _reason = reason;
        _invitor = invitor;
        _veaEvent = veaEvent;
    }

    public static Result<RequestToJoin> Create(StatusType statusType, string reason, Guest invitor, VeaEvent veaEvent)
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
