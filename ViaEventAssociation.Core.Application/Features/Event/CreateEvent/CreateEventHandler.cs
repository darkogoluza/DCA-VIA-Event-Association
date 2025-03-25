using ViaEventAssociation.Core.Application.Common.CommandHandler;
using VIAEventAssociation.Core.Domain.Common.Repositories;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Features.Event.CreateEvent;

public class CreateEventHandler : ICommandHandler<CreateEventCommand>
{
    private readonly IEventRepository _eventRepository;
    private readonly IUnitOfWork _uow;

    public CreateEventHandler(IEventRepository eventRepository, IUnitOfWork uow)
    {
        _eventRepository = eventRepository;
        _uow = uow;
    }

    public async Task<Result<None>> HandleAsync(CreateEventCommand command)
    {
        await _eventRepository.AddAsync(command.VeaEvent);

        await _uow.SaveChangesAsync();
        return Result<None>.Success();
    }
}
