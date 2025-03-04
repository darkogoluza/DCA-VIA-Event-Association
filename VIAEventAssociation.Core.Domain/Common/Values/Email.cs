using System.Text.RegularExpressions;
using VIAEventAssociation.Core.Domain.Common.Bases;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace VIAEventAssociation.Core.Domain.Aggregates.Guests.Values;

public class Email : ValueObject
{
    public string Value { get; }

    private Email(string value)
    {
        Value = value;
    }

    public static Result<Email> Create(string value)
    {
        
        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        Match match = regex.Match(value);
        if(!match.Success)
            return Error.BadInput("Email input is invalid.");

        if(!value.Contains("@via.dk", StringComparison.OrdinalIgnoreCase))
            return Error.BadInput("Email is not VIA email");
        
        return new Email(value);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    } 
}
