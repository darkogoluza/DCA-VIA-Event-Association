using ViaEventAssociation.Core.QueryContracts.Contract;

namespace ViaEventAssociation.Core.QueryContracts.Queries;

public class GuestOverview
{
    public record Query(string GuestId) : IQuery<Answer>;

    public record Answer(Guest Guest, IEnumerable<Participation> Participations, IEnumerable<Invitation> Invitations, IEnumerable<JoinRequest> JoinRequests);

    public record Guest(string Name, string ProfilePictureUrl);

    public record Participation(string EventName);

    public record Invitation(string EventName);

    public record JoinRequest(string Status, string EventName);
}
