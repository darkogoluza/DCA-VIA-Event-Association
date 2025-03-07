using VIAEventAssociation.Core.Domain.Common.Bases;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace VIAEventAssociation.Core.Domain.Aggregates.Events.Values;

public class Reasion : ValueObject
{
    public string Value { get; }

    private Reasion(string value)
    {
        Value = value;
    }

    public static Result<Reasion> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Error.BadInput("Reasion name cannot be empty.");

        return new Reasion(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
