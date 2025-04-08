using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ViaEventAssociation.Infrastructure.SqliteDmPersistence;

public class DesignTimeContextFactory : IDesignTimeDbContextFactory<SqliteDmContext>
{
    public SqliteDmContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SqliteDmContext>();
        optionsBuilder.UseSqlite(@"Data Source = VEADatabaseProduction.db");
        return new SqliteDmContext(optionsBuilder.Options);
    }
}
