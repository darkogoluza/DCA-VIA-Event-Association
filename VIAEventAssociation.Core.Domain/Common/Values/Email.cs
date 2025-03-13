using System.Text.RegularExpressions;
using VIAEventAssociation.Core.Domain.Common.Bases;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace VIAEventAssociation.Core.Domain.Common.Values;

public class Email : ValueObject
{
    public string Value { get; }

    private Email(string value)
    {
        Value = value;
    }

    public static Result<Email> Create(string value)
    {

        Regex regex = new Regex(@"^([a-z\-]{3,6})@([a-z\-]+)((\.[a-z]{2,3})+)$");
        Match match = regex.Match(value);
        if (!match.Success)
            return Error.WrongEmailFormat();

        if (!value.Contains("@via.dk", StringComparison.OrdinalIgnoreCase))
            return Error.WrongEmailDomain();

        return new Email(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}