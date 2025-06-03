using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Core.QueryContracts.QueryDispatching;
using ViaEventAssociation.Core.Tools.ObjectMapper;
using ViaEventAssociation.Presentation.WebAPI.REPRBase;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Queries;

public class BrowseUpcomingEventsEndpoint(IQueryDispatcher dispatcher, IMapper mapper)
    : ApiEndpoint
        .WithRequest<BrowseUpcomingEventsEndpointRequest>
        .WithResponse<BrowseUpcomingEventsEndpointResponse>
{
    [HttpGet("events/browse-upcoming-events")]
    public override async Task<ActionResult<BrowseUpcomingEventsEndpointResponse>> HandleAsync(
        BrowseUpcomingEventsEndpointRequest request)
    {
        BrowseUpcomingEvents.Query query = mapper.Map<BrowseUpcomingEvents.Query>(request);
        BrowseUpcomingEvents.Answer answer = await dispatcher.DispatchAsync(query);
        BrowseUpcomingEventsEndpointResponse response = mapper.Map<BrowseUpcomingEventsEndpointResponse>(answer);
        return Ok(response);
    }
}

public record BrowseUpcomingEventsEndpointRequest(
    [FromQuery] BrowseUpcomingEventsEndpointRequest.Query QueryParams
)
{
    public record Query(
        string Search,
        int Page,
        int PageSize
    );
}

public record BrowseUpcomingEventsEndpointResponse(
    IEnumerable<BrowseUpcomingEventsEndpointResponse.Event> Events,
    int MaxNumberOfPages
)
{
    public record Event(
        string StartDateTime,
        string EventTitle,
        string EventDescription,
        bool Visibility,
        GuestsCount GuestsCount
    );

    public record GuestsCount(int AttendeesCount, int AvailableSlotsCount);
}