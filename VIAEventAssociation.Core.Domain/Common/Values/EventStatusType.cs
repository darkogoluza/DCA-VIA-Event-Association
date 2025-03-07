using ViaEventAssociation.Core.Tools.OperationResult;

namespace VIAEventAssociation.Core.Domain.Common.Values;

public class EventStatusType : Enumeration
{
    public static readonly EventStatusType Cancelled = new EventStatusType(0, "Cancelled");
    public static readonly EventStatusType Draft = new EventStatusType(1, "Draft");
    public static readonly EventStatusType Ready = new EventStatusType(2, "Ready");
    public static readonly EventStatusType Active = new EventStatusType(3, "Active");

    private EventStatusType() { }
    private EventStatusType(int value, string displayName) : base(value, displayName) { }
}
