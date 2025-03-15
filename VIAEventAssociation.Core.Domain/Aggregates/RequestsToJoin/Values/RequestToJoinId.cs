using VIAEventAssociation.Core.Domain.Common.Bases;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace VIAEventAssociation.Core.Domain.Aggregates.RequestsToJoin.Values;

public class RequestToJoinId : ValueObject
{
    public Guid Id { get; }

    private RequestToJoinId(Guid id)
    {
        Id = id;
    }

    public static Result<RequestToJoinId> Create(Guid id)
    {
        if (id == Guid.Empty)
            return Error.BadInput("RequestToJoinId cannot be empty.");

        return new RequestToJoinId(id);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
    }
}
