using ViaEventAssociation.Core.Application.Common.CommandHandler;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;
using VIAEventAssociation.Core.Domain.Common.Repositories;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Features.Event.UpdateDescription;

public class UpdateEventDescriptionHandler : ICommandHandler<UpdateEventDescriptionCommand>
{
    private readonly IEventRepository _eventRepository;

    public UpdateEventDescriptionHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }


    public async Task<Result<None>> HandleAsync(UpdateEventDescriptionCommand command)
    {
        VeaEvent veaEvent = await _eventRepository.GetAsync(command.VeaEventId.Id);
        var result = veaEvent.UpdateDescription(command.Description);
        if (result.isFailure)
            return result;

        return Result<None>.Success();
    }
}
