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

        DbContextOptionsBuilder<SqliteDmContext> dbContextOptionsBuilder = new();
        string testDbName = "Test" + Guid.NewGuid() + ".db";
        dbContextOptionsBuilder.UseSqlite(@"Data Source = " + testDbName);
        SqliteDmContext context = new(dbContextOptionsBuilder.Options);

        services.AddDbContext<SqliteDmContext>(options => options.UseSqlite(@"Data Source = " + testDbName));

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
        
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        return serviceProvider;
    }
}
