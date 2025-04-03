using VIAEventAssociation.Core.Domain.Common.Repositories;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Common.CommandDispatcher;

public class CommandSaveChanges : ICommandDispatcher
{
    private readonly ICommandDispatcher next;
    private readonly IUnitOfWork _uow;

    public CommandSaveChanges(ICommandDispatcher next, IUnitOfWork uow)
    {
        this.next = next;
        _uow = uow;
    }

    public async Task<Result<None>> DispatchAsync<TCommand>(TCommand command)
    {
        var result = await next.DispatchAsync(command);
        await _uow.SaveChangesAsync();
        return result;
    }
}
