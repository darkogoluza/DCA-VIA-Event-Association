using VIAEventAssociation.Core.Domain.Common.Bases;

namespace VIAEventAssociation.Core.Domain.Common.Repositories;

// This class was taken from teacher, -> https://github.com/TroelsMortensen/DomainCentricArchitectureCourse/blob/master/CodeExamples/DomainCenteredArchitectureExamples/src/Core/DCAExamples.Core.Domain/Common/Repositories/IGenericRepository.cs
public interface IGenericRepository<T>
    where T : AggregateRoot
{
    Task<T> GetAsync(Guid id);
    Task RemoveAsync(Guid id);
    Task AddAsync(T aggregate);
}