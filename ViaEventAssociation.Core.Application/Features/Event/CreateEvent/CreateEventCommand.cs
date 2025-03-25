using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Features.Event.CreateEvent;

public class CreateEventCommand
{
    public VeaEvent VeaEvent { get; private set; }

    public static Result<CreateEventCommand> Create()
    {
        var createResult = VeaEvent.Create();

        CreateEventCommand command = new CreateEventCommand(createResult.payload);
        Result<CreateEventCommand> result = Result<CreateEventCommand>.FromResult(createResult);

        if (result.isSuccess)
        {
            result.payload = command;
        }

        return result;
    }

    private CreateEventCommand(VeaEvent veaEvent)
    {
        VeaEvent = veaEvent;
    }
}
