using VIAEventAssociation.Core.Domain.Common.Bases;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace VIAEventAssociation.Core.Domain.Aggregates.Events.Values;

public class Description: ValueObject
{
    public string Value { get; }

    private Description(string value)
    {
        Value = value;
    }

    public static Result<Description> Create(string value)
    {
        return new Description(value);
    }


    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
