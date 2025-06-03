using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ViaEventAssociation.Infrastructure.EfcQueries;
using ViaEventAssociation.Infrastructure.SqliteDmPersistence;

namespace IntegrationTests.Factories;

internal class VeaWebApplicationFactory : WebApplicationFactory<Program>
{
    private IServiceCollection _serviceCollection;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            _serviceCollection = services;

            services.RemoveAll(typeof(DbContextOptions<SqliteDmContext>));
            services.RemoveAll(typeof(DbContextOptions<VeadatabaseProductionContext>));
            services.RemoveAll<SqliteDmContext>();
            services.RemoveAll<VeadatabaseProductionContext>();

            string connString = GetConnectionString();

            services.AddDbContext<SqliteDmContext>(options => { options.UseSqlite(connString); });
            services.AddDbContext<VeadatabaseProductionContext>(options => { options.UseSqlite(connString); });

            SetupCleanDatabase(services);
        });
    }

    private string GetConnectionString()
    {
        string testDbName = "Test" + Guid.NewGuid() + ".db";
        return "Data Source = " + testDbName;
    }

    private void SetupCleanDatabase(IServiceCollection services)
    {
        SqliteDmContext dmContext = services.BuildServiceProvider().GetService<SqliteDmContext>()!;
        dmContext.Database.EnsureDeleted();
        dmContext.Database.EnsureCreated();
    }

    protected override void Dispose(bool disposing)
    {
        SqliteDmContext context = _serviceCollection.BuildServiceProvider().GetService<SqliteDmContext>()!;
        context.Database.EnsureDeleted();
        base.Dispose(disposing);
    }
}
