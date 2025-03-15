using VIAEventAssociation.Core.Domain.Aggregates.Events.Values;
using VIAEventAssociation.Core.Domain.Aggregates.Guests.Entities;
using VIAEventAssociation.Core.Domain.Aggregates.Guests.Values;
using VIAEventAssociation.Core.Domain.Aggregates.Invitations.Entities;
using VIAEventAssociation.Core.Domain.Common.Bases;
using VIAEventAssociation.Core.Domain.Common.Values;
using VIAEventAssociation.Core.Domain.Contracts;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;

public class VeaEvent : AggregateRoot
{
    public VeaEventId VeaEventId { get; }
    internal Title? _title;
    internal Description? _description;
    internal DateTime? _startDateTime;
    internal DateTime? _endDateTime;
    internal bool? _visibility;
    internal MaxNoOfGuests? _maxNoOfGuests;
    internal EventStatusType _eventStatusType;
    private Location _location;
    internal IList<Guest> _guests;
    internal IList<Invitation> _invitations;

    private TimeSpan _oneAM = new TimeSpan(1, 0, 0);
    private TimeSpan _eightAM = new TimeSpan(8, 0, 0);

    private VeaEvent(VeaEventId id, EventStatusType eventStatusType) : base(id.Id)
    {
        VeaEventId = id;
        _eventStatusType = eventStatusType;
        _guests = new List<Guest>();
        _invitations = new List<Invitation>();
    }

    public static Result<VeaEvent> Create()
    {
        var eventIdResult = VeaEventId.Create(Guid.NewGuid());

        var veaEvent = new VeaEvent(eventIdResult.payload, EventStatusType.Draft);

        return veaEvent;
    }

    public Result<None> UpdateTitle(Title title)
    {
        if (Equals(_eventStatusType, EventStatusType.Active))
            return Error.CanNotModifyActiveEvent();

        if (Equals(_eventStatusType, EventStatusType.Cancelled))
            return Error.CanNotModifyCancelledEvent();

        if (Equals(_eventStatusType, EventStatusType.Draft) || Equals(_eventStatusType, EventStatusType.Ready))
        {
            _title = title;
            return Result<None>.Success();
        }

        return Result<None>.Failure();
    }

    public Result<None> UpdateDescription(Description description)
    {
        if (Equals(_eventStatusType, EventStatusType.Active))
            return Error.CanNotModifyActiveEvent();

        if (Equals(_eventStatusType, EventStatusType.Cancelled))
            return Error.CanNotModifyCancelledEvent();

        if (Equals(_eventStatusType, EventStatusType.Draft) || Equals(_eventStatusType, EventStatusType.Ready))
        {
            _description = description;
            return Result<None>.Success();
        }

        return Result<None>.Failure();
    }
    // TODO: combine the two methods into one!

    public Result<None> UpdateStarEndDateTime(DateTime startDateTime, DateTime endDateTime)
    {
        IList<Error> errors = new List<Error>();

        if (Equals(_eventStatusType, EventStatusType.Active))
            errors.Add(Error.CanNotModifyActiveEvent());

        if (Equals(_eventStatusType, EventStatusType.Cancelled))
            errors.Add(Error.CanNotModifyCancelledEvent());

        if (startDateTime < _startDateTime)
            errors.Add(Error.StartTimeInPast());

        if (endDateTime < startDateTime)
            errors.Add(Error.StartDateTimeIsBiggerThenEndDateTime());

        if ((endDateTime - startDateTime).TotalHours < 1)
            errors.Add(Error.EventDurationTooShort());

        if ((endDateTime - startDateTime).TotalHours > 10)
            errors.Add(Error.EventDurationTooLong());

        if (startDateTime.TimeOfDay < _eightAM)
            errors.Add(Error.StartTimeTooEarly());

        if (startDateTime.TimeOfDay > _oneAM && endDateTime.TimeOfDay > _oneAM && endDateTime.TimeOfDay < _eightAM)
            errors.Add(Error.EndTimeTooLate());

        if (SpansBetween1And8(startDateTime, endDateTime))
            errors.Add(Error.InvalidTimeSpan());

        if (errors.Any())
            return errors.ToArray();

        if (Equals(_eventStatusType, EventStatusType.Draft) || Equals(_eventStatusType, EventStatusType.Ready))
        {
            _endDateTime = endDateTime;
            _startDateTime = startDateTime;
            return Result<None>.Success();
        }

        return Result<None>.Failure();
    }

    public Result<None> SetVisibility(bool visibility)
    {
        if (visibility)
        {
            if (!Equals(_eventStatusType, EventStatusType.Draft) && !Equals(_eventStatusType, EventStatusType.Ready) &&
                !Equals(_eventStatusType, EventStatusType.Active))
                return Error.CanNotModifyCancelledEvent();

            _visibility = visibility;
            return Result<None>.Success();
        }

        if (Equals(_eventStatusType, EventStatusType.Draft) || Equals(_eventStatusType, EventStatusType.Ready))
        {
            _visibility = visibility;
            return Result<None>.Success();
        }

        if (Equals(_eventStatusType, EventStatusType.Cancelled))
            return Error.CanNotModifyCancelledEvent();

        if (Equals(_eventStatusType, EventStatusType.Active))
            return Error.CanNotModifyActiveEvent();

        return Result<None>.Failure();
    }

    public Result<None> SetMaxNoOfGuests(MaxNoOfGuests maxNoOfGuests)
    {
        if (Equals(_eventStatusType, EventStatusType.Cancelled))
            return Error.CanNotModifyCancelledEvent();

        if (!Equals(_eventStatusType, EventStatusType.Draft) && !Equals(_eventStatusType, EventStatusType.Ready) &&
            !Equals(_eventStatusType, EventStatusType.Active))
            return Result<None>.Failure();

        if (_maxNoOfGuests != null && _maxNoOfGuests.Value > maxNoOfGuests.Value)
            return Error.CanNotReduceMaxNoOfGuestsOnActiveEvent();

        _maxNoOfGuests = maxNoOfGuests;
        return Result<None>.Success();
    }

    public Result<None> Readie(CurrentDateTime? currentDateTime = null)
    {
        if (currentDateTime == null)
            currentDateTime = () => DateTime.Now;

        IList<Error> errors = new List<Error>();

        if (_title == null)
            errors.Add(Error.TitleNotSet());

        if (_description == null)
            errors.Add(Error.DescriptionNotSet());

        if (_maxNoOfGuests == null)
            errors.Add(Error.MaximumNumberOfGuestsIsNotSet());

        if (_visibility == null)
            errors.Add(Error.VisibilityIsNotSet());

        if (_startDateTime == null || _endDateTime == null)
            errors.Add(Error.TimesAreNotSet());

        if (Equals(_eventStatusType, EventStatusType.Cancelled))
            errors.Add(Error.CanNotModifyCancelledEvent());

        DateTime now = currentDateTime();
        if (now > _startDateTime)
            errors.Add(Error.EventIsInPast());


        if (errors.Any())
            return errors.ToArray();

        _eventStatusType = EventStatusType.Ready;
        return Result<None>.Success();
    }

    public Result<None> Activate()
    {
        if (Equals(EventStatusType.Ready, _eventStatusType) || Equals(EventStatusType.Active, _eventStatusType))
        {
            _eventStatusType = EventStatusType.Active;
            return Result<None>.Success();
        }

        if (Equals(EventStatusType.Cancelled, _eventStatusType))
            return Error.CanNotModifyCancelledEvent();

        return Error.CanNotActivateEventThatIsNotReady();
    }

    public Result<None> Participate(Guest guest)
    {
        if (Equals(EventStatusType.Draft, _eventStatusType))
            return Error.CanNotJoinDraftEvent();

        if (Equals(EventStatusType.Cancelled, _eventStatusType))
            return Error.CanNotJoinCancelledEvent();

        if (_visibility == false)
            return Error.CanNotJoinPrivateEvent();

        if ((_guests.Count + _invitations.Count) >= _maxNoOfGuests?.Value)
            return Error.CanNotJoinEventIsFull();

        if (_guests.FirstOrDefault(g => g.GuestId == guest.GuestId) != null)
            return Error.GuestIsAlreadyParticipating();

        _guests.Add(guest);
        return Result<None>.Success();
    }

    public Result<None> CancelsParticipate(GuestId guestId)
    {
        Guest? guestToRemove = _guests.FirstOrDefault(g => g.GuestId == guestId);
        if (guestToRemove != null)
        {
            _guests.Remove(guestToRemove);
        }

        return Result<None>.Success();
    }

    public Result<None> Invite(Invitation invitation)
    {
        if (Equals(EventStatusType.Draft, _eventStatusType))
            return Error.CanNotInviteToDraftEvent();

        if (Equals(EventStatusType.Cancelled, _eventStatusType))
            return Error.CanNotInviteToCancelledEvent();

        if ((_guests.Count + _invitations.Count) >= _maxNoOfGuests?.Value)
            return Error.CanNotInviteEventIsFull();

        if (_invitations.FirstOrDefault(i => i._inviteeId == invitation._inviteeId) != null)
            return Error.GuestAlreadyInvited();

        if (_guests.FirstOrDefault(g => g.GuestId == invitation._inviteeId) != null)
            return Error.GuestIsAlreadyParticipating();

        _invitations.Add(invitation);
        return Result<None>.Success();
    }

    public Result<None> AcceptInvitation(Guest guest, CurrentDateTime? currentDateTime = null)
    {
        if (currentDateTime == null)
            currentDateTime = () => DateTime.Now;

        if (Equals(EventStatusType.Cancelled, _eventStatusType))
            return Error.CanNotAcceptInvitationOnCancelledEvent();

        if (Equals(EventStatusType.Ready, _eventStatusType))
            return Error.CanNotAcceptInvitationOnReadiedEvent();

        if ((_guests.Count + _invitations.Count) >= _maxNoOfGuests?.Value)
            return Error.CanNotAcceptInvitationEventIsFull();

        var invitation = _invitations.FirstOrDefault(i => i._inviteeId == guest.GuestId);
        if (invitation == null)
            return Error.InvitationNotFound();

        DateTime now = currentDateTime();
        if (now > _startDateTime)
            return Error.EventIsInPast();

        invitation._statusType = StatusType.Accepted;
        return Result<None>.Success();
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

    private bool SpansBetween1And8(DateTime start, DateTime end)
    {
        if (start.TimeOfDay > end.TimeOfDay)
            return start.TimeOfDay <= _oneAM || end.TimeOfDay >= _eightAM;

        return start.TimeOfDay < _eightAM && end.TimeOfDay > _oneAM;
    }
}
