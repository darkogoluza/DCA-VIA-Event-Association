using ViaEventAssociation.Core.Application.Common.CommandHandler;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;
using VIAEventAssociation.Core.Domain.Common.Repositories;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Features.Event.ReadieEvent;

public class ReadieEventHandler : ICommandHandler<ReadieEventCommand>
{
    private readonly IEventRepository _eventRepository;

    public ReadieEventHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<Result<None>> HandleAsync(ReadieEventCommand command)
    {
        VeaEvent veaEvent = await _eventRepository.GetAsync(command.VeaEventId.Id);
        var result = veaEvent.Readie(command.currentDateTime);
        if (result.isFailure)
            return result;

        return Result<None>.Success();
    }
}
