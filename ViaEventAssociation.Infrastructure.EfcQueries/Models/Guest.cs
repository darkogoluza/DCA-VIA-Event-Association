namespace ViaEventAssociation.Infrastructure.EfcQueries.Models;

public partial class Guest
{
    public string Id { get; set; } = null!;

    public string ProfilePictureUrl { get; set; } = null!;

    public string? EventId { get; set; }

    public string Email { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public virtual Event? Event { get; set; }

    public virtual ICollection<Invitation> Invitations { get; set; } = new List<Invitation>();

    public virtual ICollection<RequestToJoin> RequestToJoins { get; set; } = new List<RequestToJoin>();
}
