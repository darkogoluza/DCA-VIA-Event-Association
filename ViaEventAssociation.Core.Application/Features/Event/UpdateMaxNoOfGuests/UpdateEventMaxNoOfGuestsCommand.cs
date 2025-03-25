using VIAEventAssociation.Core.Domain.Aggregates.Events.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Features.Event.UpdateMaxNoOfGuests;

public class UpdateEventMaxNoOfGuestsCommand
{
    public VeaEventId VeaEventId { get; private set; }
    public MaxNoOfGuests MaxNoOfGuests { get; private set; }

    public static Result<UpdateEventMaxNoOfGuestsCommand> Create(Guid id, int maxNoOfGuests)
    {
        var veaEventIdResult = VeaEventId.Create(id);
        var maxNoOfGuestsResult = MaxNoOfGuests.Create(maxNoOfGuests);
        UpdateEventMaxNoOfGuestsCommand command =
            new UpdateEventMaxNoOfGuestsCommand(veaEventIdResult.payload, maxNoOfGuestsResult.payload);
        Result<UpdateEventMaxNoOfGuestsCommand> result = Result<UpdateEventMaxNoOfGuestsCommand>
            .FromResult(veaEventIdResult)
            .WithResult(maxNoOfGuestsResult);

        if (result.isSuccess)
        {
            result.payload = command;
        }

        return result;
    }

    private UpdateEventMaxNoOfGuestsCommand(VeaEventId veaEventId, MaxNoOfGuests maxNoOfGuests)
    {
        VeaEventId = veaEventId;
        MaxNoOfGuests = maxNoOfGuests;
    }
}
