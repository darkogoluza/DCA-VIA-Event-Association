﻿using System.Runtime.CompilerServices;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Values;
using VIAEventAssociation.Core.Domain.Aggregates.Guests.Entities;
using VIAEventAssociation.Core.Domain.Aggregates.Invitations.Entities;
using VIAEventAssociation.Core.Domain.Common.Bases;
using VIAEventAssociation.Core.Domain.Common.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;

public class VeaEvent: AggregateRoot
{
    public EventId EventId { get; }
    internal Title _title;
    internal Description _description;
    internal DateTime _startDateTime;
    internal DateTime _endDateTime;
    internal bool _visibility;
    internal MaxNoOfGuests _maxNoOfGuests;
    internal EventStatusType _eventStatusType;
    private Location _location;
    private IList<Guest> _guests;
    private IList<Invitation> _invitations;

    private VeaEvent(EventId id, Title title, Description description, DateTime startDateTime, DateTime endDateTime, bool visibility, MaxNoOfGuests maxNoOfGuests, EventStatusType eventStatusType) : base(id.Id)
    {
        EventId = id;
        _title = title;
        _description = description;
        _startDateTime = startDateTime;
        _endDateTime = endDateTime;
        _visibility = visibility;
        _maxNoOfGuests = maxNoOfGuests;
        _eventStatusType = eventStatusType;
        _guests = new List<Guest>();
        _invitations = new List<Invitation>();
    }

    public static Result<VeaEvent> Create(string title, string description, DateTime startDateTime,
        DateTime endDateTime)
    {
        var titleResult = Title.Create(title);
        var descriptionResult = Description.Create(description);
        var maxNoOfGuestsResult = MaxNoOfGuests.Create(5);
        var eventIdResult = EventId.Create(Guid.NewGuid());
        
        var errors = new List<Error>();
        
        if (titleResult.isFailure)
            errors.AddRange(titleResult.errors);

        if (descriptionResult.isFailure)
            errors.AddRange(descriptionResult.errors);
        
        if (eventIdResult.isFailure)
            errors.AddRange(descriptionResult.errors);


        if (errors.Any())
            return errors.ToArray();

        var veaEvent = new VeaEvent(eventIdResult.payload, titleResult.payload, descriptionResult.payload, startDateTime, endDateTime, false, maxNoOfGuestsResult.payload, EventStatusType.Draft);
        return veaEvent;
    }

    public Result<VeaEvent> UpdateTitle(Title title)
    {
        if (Equals(_eventStatusType, EventStatusType.Active))
            return Error.CanNotModifyActiveEvent();
        
        if (Equals(_eventStatusType, EventStatusType.Cancelled))
            return Error.CanNotModifyCancelledEvent();
        
        if (Equals(_eventStatusType, EventStatusType.Draft) || Equals(_eventStatusType,EventStatusType.Ready))
            _title = title;

        return this;
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
        _eventStatusType = EventStatusType.Ready;
        return Result<None>.Success();
    }
    
    public Result<None> Activate()
    {
        _eventStatusType = EventStatusType.Active;
        return Result<None>.Success();
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
        _eventStatusType = EventStatusType.Cancelled;
        return Result<None>.Success();
    }
    
    public Result<None> Delete()
    {
        throw new NotImplementedException();
    }
}
