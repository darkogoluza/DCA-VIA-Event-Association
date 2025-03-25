using ViaEventAssociation.Core.Application.Common.CommandHandler;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;
using VIAEventAssociation.Core.Domain.Common.Repositories;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Features.Event.ActivateEvent;

public class ActivateEventHandler : ICommandHandler<ActivateEventCommand>
{
    private readonly IEventRepository _eventRepository;
    private readonly IUnitOfWork _uow;

    public ActivateEventHandler(IEventRepository eventRepository, IUnitOfWork uow)
    {
        _eventRepository = eventRepository;
        _uow = uow;
    }

    public async Task<Result<None>> HandleAsync(ActivateEventCommand command)
    {
        VeaEvent veaEvent = await _eventRepository.GetAsync(command.VeaEventId.Id);
        var result = veaEvent.Activate();
        if (result.isFailure)
            return result;

        await _uow.SaveChangesAsync();
        return Result<None>.Success();
    }
}
