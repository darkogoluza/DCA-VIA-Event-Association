using VIAEventAssociation.Core.Domain.Common.Repositories;

namespace UnitTests.Fakes;

public class FakeUoW : IUnitOfWork
{
    public Task SaveChangesAsync()
    {
        return Task.CompletedTask;
    }
}
