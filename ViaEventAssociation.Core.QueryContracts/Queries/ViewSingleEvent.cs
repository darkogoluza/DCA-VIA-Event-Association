using ViaEventAssociation.Core.QueryContracts.Contract;

namespace ViaEventAssociation.Core.QueryContracts.Queries;

public class ViewSingleEvent
{
    public record Query(string EventId, int Offset, int Limit) : IQuery<Answer>;

    public record Answer(
        Event Event,
        GuestsCount GuestsCount,
        GuestList GuestList
    );

    public record Event(
        string Title,
        string Description,
        string LocationName,
        string StartDateTime,
        string EndDateTime,
        bool Visibility
    );

    public record GuestsCount(int AttendeesCount, int AvailableSlotsCount);

    public record GuestList(IEnumerable<Guest> Guests);

    public record Guest(string ProfilePictureUrl, string FullName);
}
