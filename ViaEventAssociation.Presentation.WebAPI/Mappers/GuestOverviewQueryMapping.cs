using ViaEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Core.Tools.ObjectMapper;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Queries;

namespace ViaEventAssociation.Presentation.WebAPI.Mappers;

public class GuestOverviewQueryMapping : IMappingConfig<GuestOverview.Query, GuestOverviewEndpointRequest>
{
    public GuestOverviewEndpointRequest Map(GuestOverview.Query input)
    {
        return new GuestOverviewEndpointRequest(
            new GuestOverviewEndpointRequest.Route(input.GuestId)
        );
    }
}