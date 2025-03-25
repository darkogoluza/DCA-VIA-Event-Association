using ViaEventAssociation.Core.Application.Common.CommandHandler;
using VIAEventAssociation.Core.Domain.Common.Repositories;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Features.Guest.RegisterGuest;

public class RegisterGuestHandler : ICommandHandler<RegisterGuestCommand>
{
    private readonly IGuestRepository _guestRepository;
    private readonly IUnitOfWork _uow;

    public RegisterGuestHandler(IGuestRepository guestRepository, IUnitOfWork uow)
    {
        _guestRepository = guestRepository;
        _uow = uow;
    }

    public async Task<Result<None>> HandleAsync(RegisterGuestCommand command)
    {
        await _guestRepository.AddAsync(command.Guest);

        await _uow.SaveChangesAsync();
        return Result<None>.Success();
    }
}