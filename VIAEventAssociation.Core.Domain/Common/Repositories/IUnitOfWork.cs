namespace VIAEventAssociation.Core.Domain.Common.Repositories;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}