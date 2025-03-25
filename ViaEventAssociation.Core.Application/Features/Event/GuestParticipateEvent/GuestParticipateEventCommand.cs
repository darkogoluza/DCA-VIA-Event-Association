using VIAEventAssociation.Core.Domain.Aggregates.Events.Values;
using VIAEventAssociation.Core.Domain.Aggregates.Guests.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Features.Event.GuestParticipateEvent;

public class GuestParticipateEventCommand
{
    public VeaEventId VeaEventId { get; private set; }
    public GuestId GuestId { get; private set; }

    public static Result<GuestParticipateEventCommand> Create(Guid eventId, Guid guestId)
    {
        var veaEventIdResult = VeaEventId.Create(eventId);
        var guestIdResult = GuestId.Create(guestId);
        GuestParticipateEventCommand command =
            new GuestParticipateEventCommand(veaEventIdResult.payload, guestIdResult.payload);
        Result<GuestParticipateEventCommand> result = Result<GuestParticipateEventCommand>
            .FromResult(veaEventIdResult)
            .WithResult(guestIdResult);

        if (result.isSuccess)
        {
            result.payload = command;
        }

        return result;
    }

    public GuestParticipateEventCommand(VeaEventId veaEventId, GuestId guestId)
    {
        VeaEventId = veaEventId;
        GuestId = guestId;
    }
}
