using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ViaEventAssociation.Core.Application.Common.CommandHandler;

namespace ViaEventAssociation.Core.Application.Common;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommandHandlers(this IServiceCollection services, Assembly assembly)
    {
        var commandHandlerInterfaceType = typeof(ICommandHandler<>);

        var handlers = assembly.GetTypes()
            .Where(type => type.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == commandHandlerInterfaceType)
            )
            .ToList();

        foreach (var handler in handlers)
        {
            var implementedInterface = handler.GetInterfaces()
                .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == commandHandlerInterfaceType);

            services.AddScoped(implementedInterface, handler);
        }

        return services;
    } 
}
