using VIAEventAssociation.Core.Domain.Aggregates.Events.Values;
using VIAEventAssociation.Core.Domain.Contracts;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Features.Event.ReadieEvent;

public class ReadieEventCommand
{
    public VeaEventId VeaEventId { get; private set; }
    public CurrentDateTime? currentDateTime;

    public static Result<ReadieEventCommand> Create(Guid id, CurrentDateTime? currentDateTime = null)
    {
        var veaEventIdResult = VeaEventId.Create(id);
        ReadieEventCommand command = new ReadieEventCommand(veaEventIdResult.payload, currentDateTime);
        Result<ReadieEventCommand> result = Result<ReadieEventCommand>
            .FromResult(veaEventIdResult);

        if (result.isSuccess)
        {
            result.payload = command;
        }

        return result;
    }

    private ReadieEventCommand(VeaEventId veaEventId, CurrentDateTime? currentDateTime)
    {
        VeaEventId = veaEventId;
        this.currentDateTime = currentDateTime;
    }
}
