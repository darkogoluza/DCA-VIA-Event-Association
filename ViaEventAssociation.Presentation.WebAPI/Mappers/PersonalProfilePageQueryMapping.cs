using ViaEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Core.Tools.ObjectMapper;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Queries;

namespace ViaEventAssociation.Presentation.WebAPI.Mappers;

public class PersonalProfilePageQueryMapping : IMappingConfig<PersonalProfilePage.Query, PersonalProfilePageEndpointRequest>
{
    public PersonalProfilePageEndpointRequest Map(PersonalProfilePage.Query input)
    {
        return new PersonalProfilePageEndpointRequest(
            new PersonalProfilePageEndpointRequest.Route(input.GuestId)
        );
    }
}