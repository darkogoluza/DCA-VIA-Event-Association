namespace ViaEventAssociation.Infrastructure.EfcQueries.Models;

public partial class RequestToJoin
{
    public string Id { get; set; } = null!;

    public string? InvitorId { get; set; }

    public string? VeaEventId { get; set; }

    public string Reason { get; set; } = null!;

    public string StatusType { get; set; } = null!;

    public virtual Guest? Invitor { get; set; }

    public virtual Event? VeaEvent { get; set; }
}
