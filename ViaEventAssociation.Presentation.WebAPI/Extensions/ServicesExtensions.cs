using Microsoft.EntityFrameworkCore;
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
using VIAEventAssociation.Core.Domain.Contracts;
using ViaEventAssociation.Core.QueryContracts.Contract;
using ViaEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Core.QueryContracts.QueryDispatching;
using ViaEventAssociation.Infrastructure.EfcQueries.Queries;
using ViaEventAssociation.Infrastructure.SqliteDmPersistence;
using ViaEventAssociation.Infrastructure.SqliteDmPersistence.GuestPersistence;
using ViaEventAssociation.Infrastructure.SqliteDmPersistence.UnitOfWork;
using ViaEventAssociation.Infrastructure.SqliteDmPersistence.VeaEventPersistence;

namespace ViaEventAssociation.Presentation.WebAPI.Extensions;

public static class ServicesExtensions
{
    public static void RegisterDispatcher(this IServiceCollection services)
    {
        services.AddScoped<CommandDispatcher>();
        services.AddScoped<ICommandDispatcher>(sp =>
            new CommandSaveChanges(
                sp.GetRequiredService<CommandDispatcher>(),
                sp.GetRequiredService<IUnitOfWork>()
            )
        );
        services.AddScoped<IQueryDispatcher, QueryDispatcher>();
    }

    public static void RegisterCommandHandlers(this IServiceCollection services)
    {
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
        services
            .AddScoped<ICommandHandler<GuestCancelsParticipationEventCommand>, GuestCancelsParticipationEventHandler>();
        services.AddScoped<ICommandHandler<GuestInvitedCommand>, GuestInvitationHandler>();
        services.AddScoped<ICommandHandler<GuestAcceptsInvitationCommand>, GuestAcceptsInvitationHandler>();
        services.AddScoped<ICommandHandler<GuestDeclineInvitationCommand>, GuestDeclineInvitationHandler>();
    }

    public static void RegisterCQRS(this IServiceCollection services)
    {
        services
            .AddScoped<IQueryHandler<PersonalProfilePage.Query, PersonalProfilePage.Answer>,
                PersonalProfilePageHandler>();
        services
            .AddScoped<IQueryHandler<BrowseUpcomingEvents.Query, BrowseUpcomingEvents.Answer>,
                BrowseUpcomingEventsHandler>();
        services.AddScoped<IQueryHandler<ViewSingleEvent.Query, ViewSingleEvent.Answer>, ViewSingleEventHandler>();
        services
            .AddScoped<IQueryHandler<EventsEditingOverview.Query, EventsEditingOverview.Answer>,
                EventsEditingOverviewHandler>();
        services.AddScoped<IQueryHandler<GuestOverview.Query, GuestOverview.Answer>, GuestOverviewHandler>();
    }

    public static void RegisterCurrentDateTime(this IServiceCollection services)
    {
        services.AddSingleton<CurrentDateTime>(() => DateTime.Now);
    }

    public static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IEventRepository, VeaEventSqliteRepository>();
        services.AddScoped<IGuestRepository, GuestSqliteRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    public static void RegisterDatabase(this IServiceCollection services)
    {
        const string dbName = @"C:\Users\Darko\Desktop\VIA\Semester 7\DCA\VIAEventAssociation\ViaEventAssociation.Infrastructure.SqliteDmPersistence\VEADatabaseProduction.db";
        var dbContextOptionsBuilder = new DbContextOptionsBuilder<SqliteDmContext>();
        dbContextOptionsBuilder.UseSqlite($"Data Source={dbName}");

        SqliteDmContext context = new(dbContextOptionsBuilder.Options);

        services.AddDbContext<SqliteDmContext>(options =>
            options.UseSqlite($"Data Source={dbName}"));
    }
}
