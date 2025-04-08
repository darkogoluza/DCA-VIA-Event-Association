using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;
using VIAEventAssociation.Core.Domain.Common.Repositories;

namespace UnitTests.Stubs;

public class InMemEventRepoStub : IEventRepository
{
    public List<VeaEvent> Events { get; } = new();

    public Task<VeaEvent> GetAsync(Guid id)
    {
        return Task.FromResult(Events.First(e => e.VeaEventId.Id == id));
    }

    public Task RemoveAsync(Guid id)
    {
        Events.RemoveAll(e => e.VeaEventId.Id == id);
        return Task.CompletedTask;
    }

    public Task AddAsync(VeaEvent aggregate)
    {
        Events.Add(aggregate);
        return Task.CompletedTask;
    }

    public Task<ICollection<VeaEvent>> GetAllAsync()
    {
        return Task.FromResult<ICollection<VeaEvent>>(Events);
    }
}
