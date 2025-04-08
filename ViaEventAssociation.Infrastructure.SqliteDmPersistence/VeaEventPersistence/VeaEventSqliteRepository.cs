using Microsoft.EntityFrameworkCore;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Values;
using VIAEventAssociation.Core.Domain.Common.Repositories;

namespace ViaEventAssociation.Infrastructure.SqliteDmPersistence.VeaEventPersistence;

public class VeaEventSqliteRepository : RepositoryBase<VeaEvent>, IEventRepository
{
    private readonly SqliteDmContext _context;

    public VeaEventSqliteRepository(SqliteDmContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<VeaEvent> GetAsync(Guid id)
    {
        return await _context.Events.SingleAsync(veaEvent => veaEvent.VeaEventId == VeaEventId.FromGuid(id));
    }

    public async Task<ICollection<VeaEvent>> GetAllAsync()
    {
        return await _context.Events.ToListAsync();
    }
}
