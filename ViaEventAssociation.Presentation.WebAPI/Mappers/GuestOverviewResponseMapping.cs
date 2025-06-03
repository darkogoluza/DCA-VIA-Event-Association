using ViaEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Core.Tools.ObjectMapper;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Queries;

namespace ViaEventAssociation.Presentation.WebAPI.Mappers;

public class GuestOverviewResponseMapping : IMappingConfig<GuestOverview.Answer, GuestOverviewEndpointResponse>
{
    public GuestOverviewEndpointResponse Map(GuestOverview.Answer input)
    {
        return new GuestOverviewEndpointResponse(
            new GuestOverviewEndpointResponse.GuestData(input.Guest.Name, input.Guest.ProfilePictureUrl),
            input.Participations.Select(p => new GuestOverviewEndpointResponse.Participation(p.EventName)),
            input.Invitations.Select(i => new GuestOverviewEndpointResponse.Invitation(i.EventName)),
            input.JoinRequests.Select(jr => new GuestOverviewEndpointResponse.JoinRequest(jr.Status, jr.EventName))
        );
    }
}