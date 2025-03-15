using VIAEventAssociation.Core.Domain.Common.Bases;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace VIAEventAssociation.Core.Domain.Aggregates.Invitations.Values;

public class InvitationId : ValueObject
{
    public Guid Id { get; }

    private InvitationId(Guid id)
    {
        Id = id;
    }

    public static Result<InvitationId> Create(Guid id)
    {
        if (id == Guid.Empty)
            return Error.BadInput("InvitationId cannot be empty.");

        return new InvitationId(id);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
    }
}
