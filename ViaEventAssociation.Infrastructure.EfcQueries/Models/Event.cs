namespace ViaEventAssociation.Infrastructure.EfcQueries.Models;

public partial class Event
{
    public string Id { get; set; } = null!;

    public string? EndDateTime { get; set; }

    public string? StartDateTime { get; set; }

    public int? Visibility { get; set; }

    public string EventStatusType { get; set; } = null!;

    public string? Description { get; set; }

    public int? MaxNoOfGuests { get; set; }

    public string? Title { get; set; }

    public virtual ICollection<Guest> Guests { get; set; } = new List<Guest>();

    public virtual ICollection<Invitation> Invitations { get; set; } = new List<Invitation>();

    public virtual Location? Location { get; set; }

    public virtual ICollection<RequestToJoin> RequestToJoins { get; set; } = new List<RequestToJoin>();
}
