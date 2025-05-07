using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ViaEventAssociation.Core.Application.Common.CommandDispatcher;
using ViaEventAssociation.Core.Application.Common.CommandHandler;
using ViaEventAssociation.Core.Application.Features.Event.ActivateEvent;
using ViaEventAssociation.Core.Application.Features.Event.CreateEvent;
using ViaEventAssociation.Core.Application.Features.Event.GuestCancelsParticipationEvent;
using ViaEventAssociation.Core.Application.Features.Event.GuestParticipateEvent;
using ViaEventAssociation.Core.Application.Features.Event.ReadieEvent;
using ViaEventAssociation.Core.Application.Features.Event.UpdateDescription;
using ViaEventAssociation.Core.Application.Features.Event.UpdateMaxNoOfGuests;
using ViaEventAssociation.Core.Application.Features.Event.UpdateStartAndEndDateTime;
using ViaEventAssociation.Core.Application.Features.Event.UpdateTitle;
using ViaEventAssociation.Core.Application.Features.Event.UpdateVisibility;
using ViaEventAssociation.Core.Application.Features.Guest.RegisterGuest;
using ViaEventAssociation.Core.Application.Features.Invitation.AcceptInvitation;
using ViaEventAssociation.Core.Application.Features.Invitation.DeclineInvitation;
using ViaEventAssociation.Core.Application.Features.Invitation.Invate;
using VIAEventAssociation.Core.Domain.Common.Repositories;
using ViaEventAssociation.Infrastructure.SqliteDmPersistence;
using ViaEventAssociation.Infrastructure.SqliteDmPersistence.GuestPersistence;
using ViaEventAssociation.Infrastructure.SqliteDmPersistence.UnitOfWork;
using ViaEventAssociation.Infrastructure.SqliteDmPersistence.VeaEventPersistence;

namespace IntegrationTests.Common;

public static class TestServiceProviderCommandHandler
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
        services.AddScoped<IGuestRepository, GuestSqliteRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICommandHandler<CreateEventCommand>, CreateEventHandler>();
        services.AddScoped<ICommandHandler<UpdateEventTitleCommand>, UpdateEventTitleHandler>();
        services.AddScoped<ICommandHandler<UpdateEventDescriptionCommand>, UpdateEventDescriptionHandler>();
        services.AddScoped<ICommandHandler<UpdateEventVisibilityCommand>, UpdateEventVisibilityHandler>();
        services.AddScoped<ICommandHandler<UpdateEventMaxNoOfGuestsCommand>, UpdateEventMaxNoOfGuestsHandler>();
        services
            .AddScoped<ICommandHandler<UpdateEventStartAndEndDateTimeCommand>, UpdateEventStartAndEndDateTimeHandler>();
        services.AddScoped<ICommandHandler<ReadieEventCommand>, ReadieEventHandler>();
        services.AddScoped<ICommandHandler<ActivateEventCommand>, ActivateEventHandler>();
        services.AddScoped<ICommandHandler<RegisterGuestCommand>, RegisterGuestHandler>();
        services.AddScoped<ICommandHandler<GuestParticipateEventCommand>, GuestParticipateEventHandler>();
        services.AddScoped<ICommandHandler<GuestCancelsParticipationEventCommand>, GuestCancelsParticipationEventHandler>();
        services.AddScoped<ICommandHandler<GuestInvitedCommand>, GuestInvitationHandler>();
        services.AddScoped<ICommandHandler<GuestAcceptsInvitationCommand>, GuestAcceptsInvitationHandler>();
        services.AddScoped<ICommandHandler<GuestDeclineInvitationCommand>, GuestDeclineInvitationHandler>();

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
