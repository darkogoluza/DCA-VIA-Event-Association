using ViaEventAssociation.Core.Tools.OperationResult;

namespace VIAEventAssociation.Core.Domain.Common.Values;

public class StatusType : Enumeration
{
    public static readonly StatusType Pending = new("Pending");
    public static readonly StatusType Accepted = new("Accepted");
    public static readonly StatusType Rejected = new("Rejected");

    private readonly string backingValue;

    private StatusType(string value) => backingValue = value;

    private StatusType()
    {
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((StatusType)obj);
    }

    private bool Equals(StatusType other)
        => backingValue == other.backingValue;

    public override int GetHashCode()
        => backingValue.GetHashCode();
}
