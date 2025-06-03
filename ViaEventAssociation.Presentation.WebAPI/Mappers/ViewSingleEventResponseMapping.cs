using ViaEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Core.Tools.ObjectMapper;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Queries;

namespace ViaEventAssociation.Presentation.WebAPI.Mappers;

public class ViewSingleEventResponseMapping : IMappingConfig<ViewSingleEventEndpointResponse, ViewSingleEvent.Answer>
{
    public ViewSingleEvent.Answer Map(ViewSingleEventEndpointResponse input)
    {
        return new ViewSingleEvent.Answer(
            new ViewSingleEvent.Event(
                input.Event.Title,
                input.Event.Description,
                input.Event.LocationName,
                input.Event.StartDateTime,
                input.Event.EndDateTime,
                input.Event.Visibility
            ),
            new ViewSingleEvent.GuestsCount(
                input.GuestsCount.AttendeesCount,
                input.GuestsCount.AvailableSlotsCount
            ),
            new ViewSingleEvent.GuestList(
                input.GuestList.Guests.Select(g => new ViewSingleEvent.Guest(g.ProfilePictureUrl, g.FullName))
            )
        );
    }
}