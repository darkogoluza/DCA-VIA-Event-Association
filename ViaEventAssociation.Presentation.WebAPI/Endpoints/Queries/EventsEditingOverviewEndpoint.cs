using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Core.QueryContracts.QueryDispatching;
using ViaEventAssociation.Core.Tools.ObjectMapper;
using ViaEventAssociation.Presentation.WebAPI.REPRBase;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Queries;

public class EventsEditingOverviewEndpoint(IQueryDispatcher dispatcher, IMapper mapper)
    : ApiEndpoint
        .WithoutRequest
        .WithResponse<EventsEditingOverviewEndpointResponse>
{
    [HttpGet("events/editing-overview")]
    public override async Task<ActionResult<EventsEditingOverviewEndpointResponse>> HandleAsync()
    {
        EventsEditingOverview.Query query = new EventsEditingOverview.Query();
        EventsEditingOverview.Answer answer = await dispatcher.DispatchAsync(query);
        EventsEditingOverviewEndpointResponse response = mapper.Map<EventsEditingOverviewEndpointResponse>(answer);
        return Ok(response);
    }
}

public record EventsEditingOverviewEndpointResponse(
    IEnumerable<EventsEditingOverviewEndpointResponse.Event> DraftEvents,
    IEnumerable<EventsEditingOverviewEndpointResponse.Event> ReadyEvents,
    IEnumerable<EventsEditingOverviewEndpointResponse.Event> CancelledEvents
)
{
    public record Event(string EventTitle);
}
