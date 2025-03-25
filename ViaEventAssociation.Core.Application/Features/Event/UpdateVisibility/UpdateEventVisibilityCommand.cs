using VIAEventAssociation.Core.Domain.Aggregates.Events.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Features.Event.UpdateVisibility;

public class UpdateEventVisibilityCommand
{
    public VeaEventId VeaEventId { get; private set; }
    public bool Visibility { get; private set; }

    public static Result<UpdateEventVisibilityCommand> Create(Guid id, bool visibilty)
    {
        var veaEventIdResult = VeaEventId.Create(id);
        UpdateEventVisibilityCommand command = new UpdateEventVisibilityCommand(veaEventIdResult.payload, visibilty);
        Result<UpdateEventVisibilityCommand> result = Result<UpdateEventVisibilityCommand>
            .FromResult(veaEventIdResult);

        if (result.isSuccess)
        {
            result.payload = command;
        }

        return result;
    }

    private UpdateEventVisibilityCommand(VeaEventId veaEventId, bool visibility)
    {
        VeaEventId = veaEventId;
        Visibility = visibility;
    }
}
