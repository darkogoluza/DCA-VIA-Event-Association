using Microsoft.EntityFrameworkCore;
using VIAEventAssociation.Core.Domain.Common.Repositories;

namespace ViaEventAssociation.Infrastructure.SqliteDmPersistence.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _context;

    public UnitOfWork(DbContext context)
    {
        _context = context;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
