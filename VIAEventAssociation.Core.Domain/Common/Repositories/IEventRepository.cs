using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;

namespace VIAEventAssociation.Core.Domain.Common.Repositories;

public interface IEventRepository : IGenericRepository<VeaEvent>
{
    public Task<ICollection<VeaEvent>> GetAllAsync();
}
