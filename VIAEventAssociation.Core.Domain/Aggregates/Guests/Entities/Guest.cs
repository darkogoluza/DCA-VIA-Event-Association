using VIAEventAssociation.Core.Domain.Aggregates.Events.Values;
using VIAEventAssociation.Core.Domain.Aggregates.Guests.Values;
using VIAEventAssociation.Core.Domain.Common.Bases;
using VIAEventAssociation.Core.Domain.Common.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace VIAEventAssociation.Core.Domain.Aggregates.Guests.Entities;

public class Guest : AggregateRoot
{
    public GuestId GuestId { get; }
    internal FirstName _firstName;
    internal LastName _lastName;
    internal Uri _profilePictureUrl;
    internal Email _email;

    private Guest(GuestId id, FirstName firstName, LastName lastName, Email email, Uri profilePictureUrl) : base(id.Id)
    {
        _firstName = firstName;
        _lastName = lastName;
        _email = email;
        _profilePictureUrl = profilePictureUrl;
        GuestId = id;
    }

    public static Result<Guest> Create(FirstName firstName, LastName lastName, Email email, Uri profilePictureUrl)
    {
        var eventIdResult = GuestId.Create(Guid.NewGuid());

        var guest = new Guest(eventIdResult.payload, firstName, lastName, email, profilePictureUrl);

        return guest;
    }

    public Result<RequestToJoin> RequestToJoin(VeaEventId veaEventId)
    {
        throw new NotImplementedException();
    }

    public Result<None> JoinEvent(VeaEventId veaEventId)
    {
        throw new NotImplementedException();
    }

    public Result<None> LeaveEvent(VeaEventId veaEventId)
    {
        throw new NotImplementedException();
    }
}
