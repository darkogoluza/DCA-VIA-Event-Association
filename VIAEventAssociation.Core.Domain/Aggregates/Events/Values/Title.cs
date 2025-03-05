using VIAEventAssociation.Core.Domain.Common.Bases;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace VIAEventAssociation.Core.Domain.Aggregates.Events.Values;

public class Title : ValueObject
{
    public string Value { get; }

    private Title(string value)
    {
        Value = value;
    }

    public static Result<Title> Create(string value)
    {
        return new Title(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
