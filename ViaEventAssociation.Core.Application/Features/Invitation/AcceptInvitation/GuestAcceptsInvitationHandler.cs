using ViaEventAssociation.Core.Application.Common.CommandHandler;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;
using VIAEventAssociation.Core.Domain.Common.Repositories;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Features.Invitation.AcceptInvitation;

public class GuestAcceptsInvitationHandler : ICommandHandler<GuestAcceptsInvitationCommand>
{
    private readonly IEventRepository _eventRepository;
    private readonly IGuestRepository _guestRepository;

    public GuestAcceptsInvitationHandler(IEventRepository eventRepository, IGuestRepository guestRepository)
    {
        _eventRepository = eventRepository;
        _guestRepository = guestRepository;
    }

    public async Task<Result<None>> HandleAsync(GuestAcceptsInvitationCommand command)
    {
        VeaEvent veaEvent = await _eventRepository.GetAsync(command.VeaEventId.Id);
        VIAEventAssociation.Core.Domain.Aggregates.Guests.Entities.Guest guest = await _guestRepository.GetAsync(command.GuestId.Id);
        var result = veaEvent.AcceptInvitation(guest, command.CurrentDateTime);
        if (result.isFailure)
            return result;

        return Result<None>.Success();
    }
}
