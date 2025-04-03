using ViaEventAssociation.Core.Application.Common.CommandHandler;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;
using VIAEventAssociation.Core.Domain.Common.Repositories;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Features.Event.UpdateMaxNoOfGuests;

public class UpdateEventMaxNoOfGuestsHandler : ICommandHandler<UpdateEventMaxNoOfGuestsCommand>
{
    private readonly IEventRepository _eventRepository;

    public UpdateEventMaxNoOfGuestsHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<Result<None>> HandleAsync(UpdateEventMaxNoOfGuestsCommand command)
    {
        VeaEvent veaEvent = await _eventRepository.GetAsync(command.VeaEventId.Id);
        var result = veaEvent.SetMaxNoOfGuests(command.MaxNoOfGuests);
        if (result.isFailure)
            return result;

        return Result<None>.Success();
    }
}
