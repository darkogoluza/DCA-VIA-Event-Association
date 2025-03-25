using VIAEventAssociation.Core.Domain.Aggregates.Events.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Features.Event.UpdateTitle;

public class UpdateEventTitleCommand
{
    public VeaEventId VeaEventId { get; private set; }
    public Title Title { get; private set; }

    public static Result<UpdateEventTitleCommand> Create(Guid id, string title)
    {
        var veaEventIdResult = VeaEventId.Create(id);
        var titleResult = Title.Create(title);
        UpdateEventTitleCommand command = new UpdateEventTitleCommand(veaEventIdResult.payload, titleResult.payload);
        Result<UpdateEventTitleCommand> result = Result<UpdateEventTitleCommand>
            .FromResult(veaEventIdResult)
            .WithResult(titleResult);

        if (result.isSuccess)
        {
            result.payload = command;
        }

        return result;
    }

    private UpdateEventTitleCommand(VeaEventId veaEventId, Title title)
    {
        VeaEventId = veaEventId;
        Title = title;
    }
}
