namespace VIAEventAssociation.Core.Domain.Common.Bases;

// This class was taken from teacher, -> https://github.com/TroelsMortensen/DomainCentricArchitectureCourse/blob/master/CodeExamples/DomainCenteredArchitectureExamples/src/Core/DCAExamples.Core.Domain/Common/Bases/AggregateRoot.cs

public abstract class AggregateRoot : Entity
{
    protected AggregateRoot(Guid id) : base(id)
    {
    }

    protected AggregateRoot() { }

}