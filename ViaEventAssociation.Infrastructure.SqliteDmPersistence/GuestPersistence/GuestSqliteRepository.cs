using Microsoft.EntityFrameworkCore;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Values;
using VIAEventAssociation.Core.Domain.Aggregates.Guests.Entities;
using VIAEventAssociation.Core.Domain.Aggregates.Guests.Values;
using VIAEventAssociation.Core.Domain.Common.Repositories;

namespace ViaEventAssociation.Infrastructure.SqliteDmPersistence.GuestPersistence;

public class GuestSqliteRepository : RepositoryBase<Guest>, IGuestRepository
{
    private readonly SqliteDmContext _context;

    public GuestSqliteRepository(SqliteDmContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<Guest> GetAsync(Guid id)
    {
        return await _context.Guests.SingleAsync(guest => guest.GuestId == GuestId.FromGuid(id));
    }

    public async Task<ICollection<Guest>> GetAllAsync()
    {
        return await _context.Guests.ToListAsync();
    }
}
