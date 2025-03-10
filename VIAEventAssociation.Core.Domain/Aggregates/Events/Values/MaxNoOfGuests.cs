using VIAEventAssociation.Core.Domain.Common.Bases;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace VIAEventAssociation.Core.Domain.Aggregates.Events.Values;

public class MaxNoOfGuests : ValueObject
{
    public int Value { get; }

    private MaxNoOfGuests(int value)
    {
        Value = value;
    }

    public static Result<MaxNoOfGuests> Create(int value)
    {
        if (value < 5)
            return Error.MaxNoOfGuestsTooSmall();

        if (value > 50)
            return Error.MaxNoOfGuestsTooLarge();

        return new MaxNoOfGuests(value);
    }


    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
