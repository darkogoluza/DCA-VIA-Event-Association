namespace ViaEventAssociation.Infrastructure.EfcQueries.Models;

public partial class Invitation
{
    public string Id { get; set; } = null!;

    public string? EventId { get; set; }

    public string? InviteeId { get; set; }

    public string StatusType { get; set; } = null!;

    public virtual Event? Event { get; set; }

    public virtual Guest? Invitee { get; set; }
}
