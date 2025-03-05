using VIAEventAssociation.Core.Domain.Common.Bases;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace VIAEventAssociation.Core.Domain.Aggregates.Events.Values;

public class EventId: ValueObject
{
    public Guid Id { get; }

    private EventId(Guid id)
    {
        Id = id;
    }

    public static Result<EventId> Create(Guid id)
    {
        if (id == Guid.Empty)
            return Error.BadInput("EventId cannot be empty.");

        return new EventId(id);
    }
   
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
    }
}
