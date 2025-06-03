using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Core.QueryContracts.QueryDispatching;
using ViaEventAssociation.Core.Tools.ObjectMapper;
using ViaEventAssociation.Presentation.WebAPI.REPRBase;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Queries;

public class GuestOverviewEndpoint(IQueryDispatcher dispatcher, IMapper mapper)
    : ApiEndpoint
        .WithRequest<GuestOverviewEndpointRequest>
        .WithResponse<GuestOverviewEndpointResponse>
{
    [HttpGet("guest/{GuestId}/overview")]
    public override async Task<ActionResult<GuestOverviewEndpointResponse>> HandleAsync(
        GuestOverviewEndpointRequest request)
    {
        GuestOverview.Query query = mapper.Map<GuestOverview.Query>(request);
        GuestOverview.Answer answer = await dispatcher.DispatchAsync(query);
        GuestOverviewEndpointResponse response = mapper.Map<GuestOverviewEndpointResponse>(answer);
        return Ok(response);
    }
}

public record GuestOverviewEndpointRequest(
    [FromRoute] GuestOverviewEndpointRequest.Route RouteParams
)
{
    public record Route(
        string GuestId
    );
}

public record GuestOverviewEndpointResponse(
    GuestOverviewEndpointResponse.GuestData Guest,
    IEnumerable<GuestOverviewEndpointResponse.Participation> Participations,
    IEnumerable<GuestOverviewEndpointResponse.Invitation> Invitations,
    IEnumerable<GuestOverviewEndpointResponse.JoinRequest> JoinRequests
)
{
    public record GuestData(string Name, string ProfilePictureUrl);

    public record Participation(string EventName);

    public record Invitation(string EventName);

    public record JoinRequest(string Status, string EventName);
}
