using VIAEventAssociation.Core.Domain.Aggregates.Events.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Features.Event.UpdateDescription;

public class UpdateEventDescriptionCommand
{
    public VeaEventId VeaEventId { get; private set; }
    public Description Description { get; private set; }

    public static Result<UpdateEventDescriptionCommand> Create(Guid id, string description)
    {
        var veaEventIdResult = VeaEventId.Create(id);
        var descriptionResult = Description.Create(description);
        UpdateEventDescriptionCommand command = new UpdateEventDescriptionCommand(veaEventIdResult.payload, descriptionResult.payload);
        Result<UpdateEventDescriptionCommand> result = Result<UpdateEventDescriptionCommand>
            .FromResult(veaEventIdResult)
            .WithResult(descriptionResult);

        if (result.isSuccess)
        {
            result.payload = command;
        }

        return result;
    }

    private UpdateEventDescriptionCommand(VeaEventId veaEventId, Description description)
    {
        VeaEventId = veaEventId;
        Description = description;
    }
}
