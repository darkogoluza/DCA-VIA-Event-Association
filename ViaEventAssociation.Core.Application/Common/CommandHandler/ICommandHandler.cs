using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Common.CommandHandler;

public interface ICommandHandler<TCommand>
{
    Task<Result<None>> HandleAsync(TCommand command);
}