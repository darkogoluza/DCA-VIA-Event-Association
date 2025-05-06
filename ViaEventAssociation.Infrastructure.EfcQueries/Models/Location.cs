namespace ViaEventAssociation.Infrastructure.EfcQueries.Models;

public partial class Location
{
    public string LocationId { get; set; } = null!;

    public virtual Event LocationNavigation { get; set; } = null!;
}
