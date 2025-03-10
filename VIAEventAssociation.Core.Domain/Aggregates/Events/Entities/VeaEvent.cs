using VIAEventAssociation.Core.Domain.Aggregates.Events.Values;
using VIAEventAssociation.Core.Domain.Aggregates.Guests.Entities;
using VIAEventAssociation.Core.Domain.Aggregates.Invitations.Entities;
using VIAEventAssociation.Core.Domain.Common.Bases;
using VIAEventAssociation.Core.Domain.Common.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;

public class VeaEvent : AggregateRoot
{
    public VeaEventId VeaEventId { get; }
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

    private TimeSpan _oneAM = new TimeSpan(1, 0, 0);
    private TimeSpan _eightAM = new TimeSpan(8, 0, 0);

    private VeaEvent(VeaEventId id, Title title, Description description, DateTime startDateTime, DateTime endDateTime,
        bool visibility, MaxNoOfGuests maxNoOfGuests, EventStatusType eventStatusType) : base(id.Id)
    {
        VeaEventId = id;
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

    public static Result<VeaEvent> Create(Title title, Description description, DateTime startDateTime,
        DateTime endDateTime)
    {
        var maxNoOfGuestsResult = MaxNoOfGuests.Create(5);
        var eventIdResult = VeaEventId.Create(Guid.NewGuid());

        var errors = new List<Error>();

        if (eventIdResult.isFailure)
            errors.AddRange(eventIdResult.errors);

        if (errors.Any())
            return errors.ToArray();

        var veaEvent = new VeaEvent(eventIdResult.payload, title, description,
            startDateTime, endDateTime, false, maxNoOfGuestsResult.payload, EventStatusType.Draft);

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

    private bool SpansBetween1And8(DateTime start, DateTime end)
    {
        if (start.TimeOfDay > end.TimeOfDay)
            return start.TimeOfDay <= _oneAM || end.TimeOfDay >= _eightAM;

        return start.TimeOfDay < _eightAM && end.TimeOfDay > _oneAM;
    }
}
