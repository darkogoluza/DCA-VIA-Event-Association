using VIAEventAssociation.Core.Domain.Aggregates.Events.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Features.Event.ActivateEvent;

public class ActivateEventCommand
{
    public VeaEventId VeaEventId { get; private set; }

    public static Result<ActivateEventCommand> Create(Guid id)
    {
        var veaEventIdResult = VeaEventId.Create(id);
        ActivateEventCommand command = new ActivateEventCommand(veaEventIdResult.payload);
        Result<ActivateEventCommand> result = Result<ActivateEventCommand>
            .FromResult(veaEventIdResult);

        if (result.isSuccess)
        {
            result.payload = command;
        }

        return result;
    }

    private ActivateEventCommand(VeaEventId veaEventId)
    {
        VeaEventId = veaEventId;
    }
}
