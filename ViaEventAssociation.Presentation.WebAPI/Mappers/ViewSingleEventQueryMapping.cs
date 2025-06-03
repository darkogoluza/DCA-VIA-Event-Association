using ViaEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Core.Tools.ObjectMapper;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Queries;

namespace ViaEventAssociation.Presentation.WebAPI.Mappers;

public class ViewSingleEventQueryMapping : IMappingConfig<ViewSingleEvent.Query, ViewSingleEventEndpointRequest>
{
    public ViewSingleEventEndpointRequest Map(ViewSingleEvent.Query input)
    {
        return new ViewSingleEventEndpointRequest(
            new ViewSingleEventEndpointRequest.Query(input.Offset, input.Limit),
            new ViewSingleEventEndpointRequest.Route(input.EventId)
        );
    }
}