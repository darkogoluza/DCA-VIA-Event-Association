using VIAEventAssociation.Core.Domain.Common.Bases;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace VIAEventAssociation.Core.Domain.Aggregates.Guests.Values;

public class GuestId : ValueObject
{
    public Guid Id { get; }

    private GuestId(Guid id)
    {
        Id = id;
    }

    public static Result<GuestId> Create(Guid id)
    {
        if (id == Guid.Empty)
            return Error.BadInput("EventId cannot be empty.");

        return new GuestId(id);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
    }
}
