using Microsoft.EntityFrameworkCore;
using VIAEventAssociation.Core.Domain.Common.Bases;
using VIAEventAssociation.Core.Domain.Common.Repositories;

namespace ViaEventAssociation.Infrastructure.SqliteDmPersistence;

public abstract class RepositoryBase<T> : IGenericRepository<T> where T : AggregateRoot
{
    private readonly SqliteDmContext _context;

    protected RepositoryBase(SqliteDmContext context)
    {
        _context = context;
    }

    public abstract Task<T> GetAsync(Guid id);

    public async Task RemoveAsync(Guid id)
    {
        T agg = await GetAsync(id);
        _context.Set<T>().Remove(agg);
    }

    public async Task AddAsync(T aggregate)
        => await _context.Set<T>().AddAsync(aggregate);
}
