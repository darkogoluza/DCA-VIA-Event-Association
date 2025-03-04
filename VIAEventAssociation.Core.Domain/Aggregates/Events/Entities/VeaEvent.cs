using VIAEventAssociation.Core.Domain.Aggregates.Events.Values;
using VIAEventAssociation.Core.Domain.Aggregates.Guests.Entities;
using VIAEventAssociation.Core.Domain.Aggregates.Invitations.Entities;
using VIAEventAssociation.Core.Domain.Common.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;

public class VeaEvent
{
    private Title _title;
    private Description _description;
    private DateTime _startDateTime;
    private DateTime _endDateTime;
    private bool _visibility;
    private MaxNoOfGuests _maxNoOfGuests;
    private EventStatusType _eventStatusType;
    private Location _location;
    private IList<Guest> _guests;
    private IList<Invitation> _invitations;


    private VeaEvent(Title title, Description description, DateTime startDateTime, DateTime endDateTime, bool visibility, MaxNoOfGuests maxNoOfGuests, EventStatusType eventStatusType, Location location)
    {
        _title = title;
        _description = description;
        _startDateTime = startDateTime;
        _endDateTime = endDateTime;
        _visibility = visibility;
        _maxNoOfGuests = maxNoOfGuests;
        _eventStatusType = eventStatusType;
        _location = location;
        _guests = new List<Guest>();
        _invitations = new List<Invitation>();
    }

    public static Result<VeaEvent> Create(string title, string description, DateTime startDateTime,
        DateTime endDateTime, bool visibility, int maxNoOfGuests, EventStatusType eventStatusType, Location location)
    {
        throw new NotImplementedException();
    }

    public Result<VeaEvent> UpdateTitle(Title title)
    {
        throw new NotImplementedException();
    }
    
    public Result<VeaEvent> UpdateDescription(Description description)
    {
        throw new NotImplementedException();
    }
    
    public Result<VeaEvent> UpdateStartDateTime(DateTime startDateTime)
    {
        throw new NotImplementedException();
    }
    
    public Result<VeaEvent> UpdateEndDateTime(DateTime endDateTime)
    {
        throw new NotImplementedException();
    }

    public Result<VeaEvent> SetVisibility(bool visibility)
    {
        throw new NotImplementedException();
    }
    
    public Result<VeaEvent> SetMaxNoOfGuests(MaxNoOfGuests maxNoOfGuests)
    {
        throw new NotImplementedException();
    }
    
    public Result<None> Readie()
    {
        throw new NotImplementedException();
    }
    
    public Result<None> Activate()
    {
        throw new NotImplementedException();
    }
    
    public Result<None> AcceptInvitation(Guest guest, Invitation invitation)
    {
        throw new NotImplementedException();
    }
    
    public Result<None> DeclineInvitation(Guest guest, Invitation invitation)
    {
        throw new NotImplementedException();
    }
    
    public Result<None> ExtendInvitation(Guest guest, Invitation invitation)
    {
        throw new NotImplementedException();
    }

    public Result<None> Cancel()
    {
        throw new NotImplementedException();
    }
    
    public Result<None> Delete()
    {
        throw new NotImplementedException();
    }
}
