using ViaEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Core.Tools.ObjectMapper;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Queries;

namespace ViaEventAssociation.Presentation.WebAPI.Mappers;

public class PersonalProfilePageResponseMapping : IMappingConfig<PersonalProfilePageEndpointResponse, PersonalProfilePage.Answer>
{
    public PersonalProfilePage.Answer Map(PersonalProfilePageEndpointResponse input)
    {
        return new PersonalProfilePage.Answer(
            new PersonalProfilePage.Guest(
                input.Guest.Name,
                input.Guest.Email,
                input.Guest.ProfilePictureUrl
            ),
            input.UpcomingEventsCount,
            input.UpcomingEvents.Select(u => new PersonalProfilePage.UpcomingEvent(
                u.EventTitle,
                u.NumberOfAttendees,
                u.Date,
                u.StartTime
            )),
            input.PastEvents.Select(p => new PersonalProfilePage.PastEvent(
                p.EventTitle
            )),
            input.PendingInvitationsCount
        );
    }
}