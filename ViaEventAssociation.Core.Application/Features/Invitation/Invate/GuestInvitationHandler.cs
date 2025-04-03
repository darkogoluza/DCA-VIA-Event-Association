using ViaEventAssociation.Core.Application.Common.CommandHandler;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;
using VIAEventAssociation.Core.Domain.Common.Repositories;
using VIAEventAssociation.Core.Domain.Services;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Features.Invitation.Invate;

public class GuestInvitationHandler : ICommandHandler<GuestInvitedCommand>
{
    private readonly IEventRepository _eventRepository;

    public GuestInvitationHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }


    public async Task<Result<None>> HandleAsync(GuestInvitedCommand command)
    {
        VeaEvent veaEvent = await _eventRepository.GetAsync(command.VeaEventId.Id);
        GuestInviteService guestInviteService = new GuestInviteService();
        var result = guestInviteService.InviteGuest(command.GuestId, veaEvent);
        if (result.isFailure)
            return result;

        return Result<None>.Success();
    }
}