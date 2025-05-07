using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VIAEventAssociation.Core.Domain.Contracts;
using ViaEventAssociation.Core.QueryContracts.Contract;
using ViaEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Core.QueryContracts.QueryDispatching;
using ViaEventAssociation.Infrastructure.EfcQueries;
using ViaEventAssociation.Infrastructure.EfcQueries.Queries;

namespace IntegrationTests.Common;

public static class TestServiceProviderCQRS
{
    public static ServiceProvider CreateServiceProvider()
    {
        var services = new ServiceCollection();

        var context = VeadatabaseProductionContext.SetupReadContext().Seed();
        services.AddDbContext<VeadatabaseProductionContext>(options =>
        {
            options.UseSqlite(context.Database.GetDbConnection());
        });

        services.AddScoped<IQueryDispatcher, QueryDispatcher>();
        services.AddSingleton<CurrentDateTime>(() => new DateTime(2024, 4, 8, 15, 0, 0));
        services.AddScoped<IQueryHandler<PersonalProfilePage.Query, PersonalProfilePage.Answer>, PersonalProfilePageHandler>();

        var serviceProvider = services.BuildServiceProvider();

        return serviceProvider;
    }
}
