using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ViaEventAssociation.Core.Application.Common.CommandDispatcher;
using ViaEventAssociation.Core.Application.Common.CommandHandler;
using ViaEventAssociation.Core.Application.Features.Event.CreateEvent;
using VIAEventAssociation.Core.Domain.Common.Repositories;
using ViaEventAssociation.Infrastructure.SqliteDmPersistence;
using ViaEventAssociation.Infrastructure.SqliteDmPersistence.UnitOfWork;
using ViaEventAssociation.Infrastructure.SqliteDmPersistence.VeaEventPersistence;

namespace IntegrationTests.Common;

public static class TestServiceProvider
{
    public static ServiceProvider CreateServiceProvider()
    {
        var services = new ServiceCollection();

        /*
        services.AddDbContext<SqliteDmContext>(options =>
            options.UseSqlite(@"Data Source = C:\Users\Darko\Desktop\VIA\Semester 7\DCA\VIAEventAssociation\ViaEventAssociation.Infrastructure.SqliteDmPersistence\VEADatabaseProduction.db"));
         * 
         */
        services.AddDbContext<SqliteDmContext>(options =>
            options.UseInMemoryDatabase(databaseName: "InMemoryVeaEventDb"));

        services.AddScoped<IEventRepository, VeaEventSqliteRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICommandHandler<CreateEventCommand>, CreateEventHandler>();

        services.AddScoped<CommandDispatcher>();
        services.AddScoped<ICommandDispatcher>(sp =>
            new CommandSaveChanges(
                sp.GetRequiredService<CommandDispatcher>(),
                sp.GetRequiredService<IUnitOfWork>()
            )
        );

        var serviceProvider = services.BuildServiceProvider();

        return serviceProvider;
    }
}
