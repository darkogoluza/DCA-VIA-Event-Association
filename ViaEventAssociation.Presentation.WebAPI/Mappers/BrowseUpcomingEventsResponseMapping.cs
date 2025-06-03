using ViaEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Core.Tools.ObjectMapper;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Queries;

namespace ViaEventAssociation.Presentation.WebAPI.Mappers;

public class BrowseUpcomingEventsResponseMapping : IMappingConfig<BrowseUpcomingEvents.Answer, BrowseUpcomingEventsEndpointResponse>
{
    public BrowseUpcomingEventsEndpointResponse Map(BrowseUpcomingEvents.Answer input)
    {
        return new BrowseUpcomingEventsEndpointResponse(
            input.Events.Select(e => new BrowseUpcomingEventsEndpointResponse.Event(
                e.StartDateTime,
                e.EventTitle,
                e.EventDescription,
                e.Visibility,
                new BrowseUpcomingEventsEndpointResponse.GuestsCount(
                    e.GuestsCount.AttendeesCount,
                    e.GuestsCount.AvailableSlotsCount
                )
            )),
            input.MaxNumberOfPages
        );
    }
}