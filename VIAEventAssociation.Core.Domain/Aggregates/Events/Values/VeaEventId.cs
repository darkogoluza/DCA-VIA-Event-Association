using VIAEventAssociation.Core.Domain.Common.Bases;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace VIAEventAssociation.Core.Domain.Aggregates.Events.Values;

public class VeaEventId: ValueObject
{
    public Guid Id { get; }

    private VeaEventId(Guid id)
    {
        Id = id;
    }

    public static Result<VeaEventId> Create(Guid id)
    {
        if (id == Guid.Empty)
            return Error.BadInput("EventId cannot be empty.");

        return new VeaEventId(id);
    }
   
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
    }
}
