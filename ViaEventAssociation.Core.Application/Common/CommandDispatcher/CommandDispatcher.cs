using ViaEventAssociation.Core.Application.Common.CommandHandler;
using ViaEventAssociation.Core.Tools.OperationResult;
using Microsoft.Extensions.DependencyInjection;


namespace ViaEventAssociation.Core.Application.Common.CommandDispatcher;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _serviceProvider;


    public CommandDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task<Result<None>> DispatchAsync<TCommand>(TCommand command)
    {
        ICommandHandler<TCommand> service = _serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();

        return service.HandleAsync(command);
    }
}
