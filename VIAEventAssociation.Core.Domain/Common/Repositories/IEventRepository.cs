using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;

namespace VIAEventAssociation.Core.Domain.Common.Repositories;

public interface IEventRepository : IGenericRepository<VeaEvent>;
