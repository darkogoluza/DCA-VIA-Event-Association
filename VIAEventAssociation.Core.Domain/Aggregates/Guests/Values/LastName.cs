using System.Text.RegularExpressions;
using VIAEventAssociation.Core.Domain.Common.Bases;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace VIAEventAssociation.Core.Domain.Aggregates.Guests.Values;

public class LastName : ValueObject
{
    public string Value { get; }

    private LastName(string value)
    {
        Value = value;
    }

    public static Result<LastName> Create(string value)
    {
        Regex regex = new Regex(@"^[A-Z][a-z]{1,24}$");
        Match match = regex.Match(value);
        if (!match.Success)
            return Error.WrongLastNameFormat();

        return new LastName(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
