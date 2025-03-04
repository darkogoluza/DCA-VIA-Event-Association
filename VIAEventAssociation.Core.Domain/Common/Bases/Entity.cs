namespace VIAEventAssociation.Core.Domain.Common.Bases;

// This class was taken from teacher, -> https://github.com/TroelsMortensen/DomainCentricArchitectureCourse/blob/master/CodeExamples/DomainCenteredArchitectureExamples/src/Core/DCAExamples.Core.Domain/Common/Bases/Entity.cs

public abstract class Entity
{
    public Guid Id { get; }

    protected Entity(Guid id)
    {
        Id = id;
    }

    protected Entity() {}

    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType())
        {
            return false;
        }

        Entity entity = (Entity)obj;
        return entity.Id.Equals(Id);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}