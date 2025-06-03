using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Core.QueryContracts.QueryDispatching;
using ViaEventAssociation.Core.Tools.ObjectMapper;
using ViaEventAssociation.Presentation.WebAPI.REPRBase;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Queries;

public class ViewSingleEventEndpoint(IQueryDispatcher dispatcher, IMapper mapper)
    : ApiEndpoint
        .WithRequest<ViewSingleEventEndpointRequest>
        .WithResponse<ViewSingleEventEndpointResponse>
{
    [HttpGet("event/{EventId}")]
    public override async Task<ActionResult<ViewSingleEventEndpointResponse>> HandleAsync(
        ViewSingleEventEndpointRequest request)
    {
        ViewSingleEvent.Query query = mapper.Map<ViewSingleEvent.Query>(request);
        ViewSingleEvent.Answer answer = await dispatcher.DispatchAsync(query);
        ViewSingleEventEndpointResponse response = mapper.Map<ViewSingleEventEndpointResponse>(answer);
        return Ok(response);
    }
}

public record ViewSingleEventEndpointRequest(
    [FromQuery] ViewSingleEventEndpointRequest.Query QueryParams,
    [FromRoute] ViewSingleEventEndpointRequest.Route RouteParams
)
{
    public record Route(
        string EventId
    );

    public record Query(
        int Offset,
        int Limit
    );
}

public record ViewSingleEventEndpointResponse(
    ViewSingleEventEndpointResponse.EventData Event,
    ViewSingleEventEndpointResponse.GuestsCountData GuestsCount,
    ViewSingleEventEndpointResponse.GuestListData GuestList
)
{
    public record EventData(
        string Title,
        string Description,
        string LocationName,
        string StartDateTime,
        string EndDateTime,
        bool Visibility
    );

    public record GuestsCountData(int AttendeesCount, int AvailableSlotsCount);

    public record GuestListData(IEnumerable<Guest> Guests);

    public record Guest(string ProfilePictureUrl, string FullName);
}
