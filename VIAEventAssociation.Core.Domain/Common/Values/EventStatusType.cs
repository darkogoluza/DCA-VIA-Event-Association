using ViaEventAssociation.Core.Tools.OperationResult;

namespace VIAEventAssociation.Core.Domain.Common.Values;

public class EventStatusType : Enumeration
{
    public static readonly EventStatusType Cancelled = new("Cancelled");
    public static readonly EventStatusType Draft = new("Draft");
    public static readonly EventStatusType Ready = new("Ready");
    public static readonly EventStatusType Active = new("Active");

    private readonly string backingValue;

    private EventStatusType(string value)
        => backingValue = value;

    private EventStatusType()
    {
    }

    private bool Equals(EventStatusType other)
        => backingValue == other.backingValue;
    
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((EventStatusType)obj);
    }


    public override int GetHashCode()
        => backingValue.GetHashCode();
}
