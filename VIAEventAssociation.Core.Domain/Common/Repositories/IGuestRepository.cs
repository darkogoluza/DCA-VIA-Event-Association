using VIAEventAssociation.Core.Domain.Aggregates.Guests.Entities;

namespace VIAEventAssociation.Core.Domain.Common.Repositories;

public interface IGuestRepository : IGenericRepository<Guest>
{
    public Task<ICollection<Guest>> GetAllAsync();
}
