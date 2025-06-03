using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Core.QueryContracts.QueryDispatching;
using ViaEventAssociation.Core.Tools.ObjectMapper;
using ViaEventAssociation.Presentation.WebAPI.REPRBase;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Queries;

public class PersonalProfilePageEndpoint(IQueryDispatcher dispatcher, IMapper mapper)
    : ApiEndpoint
        .WithRequest<PersonalProfilePageEndpointRequest>
        .WithResponse<PersonalProfilePageEndpointResponse>
{
    [HttpGet("guest/{GuestId}/personal-profile")]
    public override async Task<ActionResult<PersonalProfilePageEndpointResponse>> HandleAsync(
        PersonalProfilePageEndpointRequest request)
    {
        PersonalProfilePage.Query query = mapper.Map<PersonalProfilePage.Query>(request);
        PersonalProfilePage.Answer answer = await dispatcher.DispatchAsync(query);
        PersonalProfilePageEndpointResponse response = mapper.Map<PersonalProfilePageEndpointResponse>(answer);
        return Ok(response);
    }
}

public record PersonalProfilePageEndpointRequest(
    [FromRoute] PersonalProfilePageEndpointRequest.Route RouteParams
)
{
    public record Route(
        string GuestId
    );
}

public record PersonalProfilePageEndpointResponse(
    PersonalProfilePageEndpointResponse.GuestData Guest,
    int UpcomingEventsCount,
    IEnumerable<PersonalProfilePageEndpointResponse.UpcomingEvent> UpcomingEvents,
    IEnumerable<PersonalProfilePageEndpointResponse.PastEvent> PastEvents,
    int PendingInvitationsCount
)
{
    public record GuestData(string Name, string Email, string ProfilePictureUrl);

    public record UpcomingEvent(string EventTitle, int NumberOfAttendees, DateOnly Date, TimeOnly StartTime);

    public record PastEvent(string EventTitle);
}
