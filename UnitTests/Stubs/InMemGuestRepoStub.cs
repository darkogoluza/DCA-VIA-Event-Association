using VIAEventAssociation.Core.Domain.Aggregates.Guests.Entities;
using VIAEventAssociation.Core.Domain.Common.Repositories;

namespace UnitTests.Stubs;

public class InMemGuestRepoStub : IGuestRepository
{
    public List<Guest> Guests { get; } = new();

    public Task<Guest> GetAsync(Guid id)
    {
        return Task.FromResult(Guests.First(g => g.GuestId.Id == id));
    }

    public Task RemoveAsync(Guid id)
    {
        Guests.RemoveAll(g => g.GuestId.Id == id);
        return Task.CompletedTask;
    }

    public Task AddAsync(Guest aggregate)
    {
        Guests.Add(aggregate);
        return Task.CompletedTask;
    }

    public Task<ICollection<Guest>> GetAllAsync()
    {
        return Task.FromResult<ICollection<Guest>>(Guests);
    }
}
