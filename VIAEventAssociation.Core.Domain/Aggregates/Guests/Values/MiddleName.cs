using VIAEventAssociation.Core.Domain.Common.Bases;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace VIAEventAssociation.Core.Domain.Aggregates.Guests.Values;

public class MiddleName : ValueObject
{
    public string Value { get; }

    private MiddleName(string value)
    {
        Value = value;
    }

    public static Result<MiddleName> Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Error.BadInput("Middle name cannot be empty.");

        return new MiddleName(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
