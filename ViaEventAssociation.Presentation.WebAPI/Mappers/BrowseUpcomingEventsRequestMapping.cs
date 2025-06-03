using ViaEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Core.Tools.ObjectMapper;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Queries;

namespace ViaEventAssociation.Presentation.WebAPI.Mappers;

public class BrowseUpcomingEventsRequestMapping : IMappingConfig<BrowseUpcomingEventsEndpointRequest, BrowseUpcomingEvents.Query>
{
    public BrowseUpcomingEvents.Query Map(BrowseUpcomingEventsEndpointRequest input)
    {
        return new BrowseUpcomingEvents.Query(
            input.QueryParams.Search,
            input.QueryParams.Page,
            input.QueryParams.PageSize
        );
    }
}
