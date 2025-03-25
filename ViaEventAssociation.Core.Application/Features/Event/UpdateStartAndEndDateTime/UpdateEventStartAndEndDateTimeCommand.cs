using VIAEventAssociation.Core.Domain.Aggregates.Events.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Features.Event.UpdateStartAndEndDateTime;

public class UpdateEventStartAndEndDateTimeCommand
{
    public VeaEventId VeaEventId { get; private set; }
    public DateTime Start { get; private set; }
    public DateTime End { get; private set; }

    public static Result<UpdateEventStartAndEndDateTimeCommand> Create(Guid id, DateTime start, DateTime end)
    {
        var veaEventIdResult = VeaEventId.Create(id);

        UpdateEventStartAndEndDateTimeCommand command = new UpdateEventStartAndEndDateTimeCommand(veaEventIdResult.payload, start, end);
        Result<UpdateEventStartAndEndDateTimeCommand> result = Result<UpdateEventStartAndEndDateTimeCommand>
            .FromResult(veaEventIdResult);

        if (result.isSuccess)
        {
            result.payload = command;
        }

        return result;
    }

    private UpdateEventStartAndEndDateTimeCommand(VeaEventId veaEventId, DateTime start, DateTime end)
    {
        VeaEventId = veaEventId;
        Start = start;
        End = end;
    }
}
