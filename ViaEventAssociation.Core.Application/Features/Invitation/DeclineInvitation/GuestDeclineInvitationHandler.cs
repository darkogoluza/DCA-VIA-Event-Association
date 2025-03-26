using ViaEventAssociation.Core.Application.Common.CommandHandler;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;
using VIAEventAssociation.Core.Domain.Common.Repositories;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Features.Invitation.DeclineInvitation;

public class GuestDeclineInvitationHandler : ICommandHandler<GuestDeclineInvitationCommand>
{
    private readonly IEventRepository _eventRepository;
    private readonly IGuestRepository _guestRepository;
    private readonly IUnitOfWork _uow;

    public GuestDeclineInvitationHandler(IEventRepository eventRepository, IGuestRepository guestRepository,
        IUnitOfWork uow)
    {
        _eventRepository = eventRepository;
        _guestRepository = guestRepository;
        _uow = uow;
    }

    public async Task<Result<None>> HandleAsync(GuestDeclineInvitationCommand command)
    {
        VeaEvent veaEvent = await _eventRepository.GetAsync(command.VeaEventId.Id);
        VIAEventAssociation.Core.Domain.Aggregates.Guests.Entities.Guest guest =
            await _guestRepository.GetAsync(command.GuestId.Id);
        var result = veaEvent.DeclineInvitation(guest);
        if (result.isFailure)
            return result;

        await _uow.SaveChangesAsync();
        return Result<None>.Success();
    }
}
