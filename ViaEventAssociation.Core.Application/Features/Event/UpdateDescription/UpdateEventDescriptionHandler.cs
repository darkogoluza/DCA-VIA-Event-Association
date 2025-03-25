using ViaEventAssociation.Core.Application.Common.CommandHandler;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;
using VIAEventAssociation.Core.Domain.Common.Repositories;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Features.Event.UpdateDescription;

public class UpdateEventDescriptionHandler : ICommandHandler<UpdateEventDescriptionCommand>
{
    private readonly IEventRepository _eventRepository;
    private readonly IUnitOfWork _uow;
    private ICommandHandler<UpdateEventDescriptionCommand> _commandHandlerImplementation;

    public UpdateEventDescriptionHandler(IEventRepository eventRepository, IUnitOfWork uow)
    {
        _eventRepository = eventRepository;
        _uow = uow;
    }


    public async Task<Result<None>> HandleAsync(UpdateEventDescriptionCommand command)
    {
        VeaEvent veaEvent = await _eventRepository.GetAsync(command.VeaEventId.Id);
        var result = veaEvent.UpdateDescription(command.Description);
        if (result.isFailure)
            return result;

        await _uow.SaveChangesAsync();
        return Result<None>.Success();

    }
}
