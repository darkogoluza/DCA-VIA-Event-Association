using ViaEventAssociation.Core.Application.Common.CommandHandler;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;
using VIAEventAssociation.Core.Domain.Common.Repositories;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Features.Event.UpdateVisibility;

public class UpdateEventVisibilityHandler : ICommandHandler<UpdateEventVisibilityCommand>
{
    private readonly IEventRepository _eventRepository;

    public UpdateEventVisibilityHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<Result<None>> HandleAsync(UpdateEventVisibilityCommand command)
    {
        VeaEvent veaEvent = await _eventRepository.GetAsync(command.VeaEventId.Id);
        var result = veaEvent.SetVisibility(command.Visibility);
        if (result.isFailure)
            return result;

        return Result<None>.Success();
    }
}
