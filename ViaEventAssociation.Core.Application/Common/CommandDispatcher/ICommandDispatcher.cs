using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Common.CommandDispatcher;

public interface ICommandDispatcher
{
    public Task<Result<None>> DispatchAsync<TCommand>(TCommand command);
}
