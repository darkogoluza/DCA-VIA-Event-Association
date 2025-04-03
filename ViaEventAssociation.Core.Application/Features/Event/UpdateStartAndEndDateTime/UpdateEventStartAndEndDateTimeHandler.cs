using ViaEventAssociation.Core.Application.Common.CommandHandler;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;
using VIAEventAssociation.Core.Domain.Common.Repositories;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Features.Event.UpdateStartAndEndDateTime;

public class UpdateEventStartAndEndDateTimeHandler : ICommandHandler<UpdateEventStartAndEndDateTimeCommand>
{
    private readonly IEventRepository _eventRepository;

    public UpdateEventStartAndEndDateTimeHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<Result<None>> HandleAsync(UpdateEventStartAndEndDateTimeCommand command)
    {
        VeaEvent veaEvent = await _eventRepository.GetAsync(command.VeaEventId.Id);
        var result = veaEvent.UpdateStarEndDateTime(command.Start, command.End);
        if (result.isFailure)
            return result;

        return Result<None>.Success();
    }
}
