using ViaEventAssociation.Core.Application.Common.CommandHandler;
using VIAEventAssociation.Core.Domain.Common.Repositories;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Features.Guest.RegisterGuest;

public class RegisterGuestHandler : ICommandHandler<RegisterGuestCommand>
{
    private readonly IGuestRepository _guestRepository;

    public RegisterGuestHandler(IGuestRepository guestRepository)
    {
        _guestRepository = guestRepository;
    }

    public async Task<Result<None>> HandleAsync(RegisterGuestCommand command)
    {
        await _guestRepository.AddAsync(command.Guest);

        return Result<None>.Success();
    }
}