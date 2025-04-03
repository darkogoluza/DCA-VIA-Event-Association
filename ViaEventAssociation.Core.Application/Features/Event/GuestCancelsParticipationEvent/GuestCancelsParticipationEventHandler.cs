using ViaEventAssociation.Core.Application.Common.CommandHandler;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;
using VIAEventAssociation.Core.Domain.Common.Repositories;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Features.Event.GuestCancelsParticipationEvent;

public class GuestCancelsParticipationEventHandler : ICommandHandler<GuestCancelsParticipationEventCommand>
{
    private readonly IEventRepository _eventRepository;

    public GuestCancelsParticipationEventHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<Result<None>> HandleAsync(GuestCancelsParticipationEventCommand command)
    {
        VeaEvent veaEvent = await _eventRepository.GetAsync(command.VeaEventId.Id);
        var result = veaEvent.CancelsParticipate(command.GuestId, command.CurrentDateTime);
        if (result.isFailure)
            return result;

        return Result<None>.Success();
    }
}
