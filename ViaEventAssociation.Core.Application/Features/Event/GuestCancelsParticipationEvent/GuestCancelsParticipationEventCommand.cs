using ViaEventAssociation.Core.Application.Features.Event.GuestParticipateEvent;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Values;
using VIAEventAssociation.Core.Domain.Aggregates.Guests.Values;
using VIAEventAssociation.Core.Domain.Contracts;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Features.Event.GuestCancelsParticipationEvent;

public class GuestCancelsParticipationEventCommand
{
    public VeaEventId VeaEventId { get; private set; }
    public GuestId GuestId { get; private set; }
    public CurrentDateTime? CurrentDateTime;

    public static Result<GuestCancelsParticipationEventCommand> Create(Guid eventId, Guid guestId,
        CurrentDateTime? currentDateTime = null)
    {
        var veaEventIdResult = VeaEventId.Create(eventId);
        var guestIdResult = GuestId.Create(guestId);
        GuestCancelsParticipationEventCommand command =
            new GuestCancelsParticipationEventCommand(veaEventIdResult.payload, guestIdResult.payload, currentDateTime);
        Result<GuestCancelsParticipationEventCommand> result = Result<GuestCancelsParticipationEventCommand>
            .FromResult(veaEventIdResult)
            .WithResult(guestIdResult);

        if (result.isSuccess)
        {
            result.payload = command;
        }

        return result;
    }

    private GuestCancelsParticipationEventCommand(VeaEventId veaEventId, GuestId guestId,
        CurrentDateTime? currentDateTime)
    {
        VeaEventId = veaEventId;
        GuestId = guestId;
        CurrentDateTime = currentDateTime;
    }
}
