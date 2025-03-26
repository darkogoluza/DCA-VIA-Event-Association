using ViaEventAssociation.Core.Application.Common.CommandHandler;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;
using VIAEventAssociation.Core.Domain.Common.Repositories;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Features.Event.GuestParticipateEvent;

public class GuestParticipateEventHandler : ICommandHandler<GuestParticipateEventCommand>
{
    private readonly IEventRepository _eventRepository;
    private readonly IGuestRepository _guestRepository;
    private readonly IUnitOfWork _uow;

    public GuestParticipateEventHandler(IEventRepository eventRepository, IGuestRepository guestRepository,
        IUnitOfWork uow)
    {
        _eventRepository = eventRepository;
        _uow = uow;
        _guestRepository = guestRepository;
    }

    public async Task<Result<None>> HandleAsync(GuestParticipateEventCommand command)
    {
        VeaEvent veaEvent = await _eventRepository.GetAsync(command.VeaEventId.Id);
        VIAEventAssociation.Core.Domain.Aggregates.Guests.Entities.Guest guest =
            await _guestRepository.GetAsync(command.GuestId.Id);
        var result = veaEvent.Participate(guest);
        if (result.isFailure)
            return result;

        await _uow.SaveChangesAsync();
        return Result<None>.Success();
    }
}
