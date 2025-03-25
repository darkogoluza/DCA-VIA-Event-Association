using ViaEventAssociation.Core.Application.Common.CommandHandler;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;
using VIAEventAssociation.Core.Domain.Common.Repositories;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Features.Event.UpdateMaxNoOfGuests;

public class UpdateEventMaxNoOfGuestsHandler : ICommandHandler<UpdateEventMaxNoOfGuestsCommand>
{
    private readonly IEventRepository _eventRepository;
    private readonly IUnitOfWork _uow;

    public UpdateEventMaxNoOfGuestsHandler(IEventRepository eventRepository, IUnitOfWork uow)
    {
        _eventRepository = eventRepository;
        _uow = uow;
    }

    public async Task<Result<None>> HandleAsync(UpdateEventMaxNoOfGuestsCommand command)
    {
        VeaEvent veaEvent = await _eventRepository.GetAsync(command.VeaEventId.Id);
        var result = veaEvent.SetMaxNoOfGuests(command.MaxNoOfGuests);
        if (result.isFailure)
            return result;

        await _uow.SaveChangesAsync();
        return Result<None>.Success();
    }
}
