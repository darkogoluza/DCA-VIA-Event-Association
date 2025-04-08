using VIAEventAssociation.Core.Domain.Common.Bases;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace VIAEventAssociation.Core.Domain.Aggregates.Events.Values;

public class VeaEventId : ValueObject
{
    public Guid Id { get;  }

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

    public static VeaEventId FromGuid(Guid guid) => new VeaEventId(guid);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
    }
    
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is null || GetType() != obj.GetType()) return false;

        return Id.Equals(((VeaEventId)obj).Id);
    }
}
