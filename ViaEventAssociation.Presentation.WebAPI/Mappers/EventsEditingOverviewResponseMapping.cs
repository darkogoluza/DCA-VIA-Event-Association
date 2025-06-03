using ViaEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Core.Tools.ObjectMapper;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Queries;

namespace ViaEventAssociation.Presentation.WebAPI.Mappers;

public class EventsEditingOverviewResponseMapping : IMappingConfig<EventsEditingOverview.Answer, EventsEditingOverviewEndpointResponse>
{
    public EventsEditingOverviewEndpointResponse Map(EventsEditingOverview.Answer input)
    {
        return new EventsEditingOverviewEndpointResponse(
            input.DraftEvents.Select(e => new EventsEditingOverviewEndpointResponse.Event(e.EventTitle)),
            input.ReadyEvents.Select(e => new EventsEditingOverviewEndpointResponse.Event(e.EventTitle)),
            input.CancelledEvents.Select(e => new EventsEditingOverviewEndpointResponse.Event(e.EventTitle))
        );
    }
}