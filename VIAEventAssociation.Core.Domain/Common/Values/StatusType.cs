using ViaEventAssociation.Core.Tools.OperationResult;

namespace VIAEventAssociation.Core.Domain.Common.Values;

public class StatusType: Enumeration
{

    public static readonly StatusType Pending = new StatusType(0, "Pending");
    public static readonly StatusType Accepted = new StatusType(1, "Accepted");
    public static readonly StatusType Rejected = new StatusType(2, "Rejected");
    
    private StatusType() {}
    private StatusType(int value, string displayName): base(value, displayName) {}
}