﻿namespace VIAEventAssociation.Core.Domain.Common.Bases;

// This class was taken from teacher, -> https://github.com/TroelsMortensen/DomainCentricArchitectureCourse/blob/master/CodeExamples/DomainCenteredArchitectureExamples/src/Core/DCAExamples.Core.Domain/Common/Bases/ValueObject.cs

public abstract class ValueObject
{
    public override bool Equals(object? other)
    {
        if (other is null) return false;
        if (other.GetType() != GetType()) return false;

        return ((ValueObject)other).GetEqualityComponents()
            .SequenceEqual(GetEqualityComponents());
    }

    protected abstract IEnumerable<object> GetEqualityComponents();

    // what are we doing here ? https://www.codingame.com/playgrounds/213/using-c-linq---a-practical-overview/aggregate
    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(obj => obj?.GetHashCode() ?? 0)
            .Aggregate((x, y) => x ^ y);
    }

    public static bool operator ==(ValueObject? left, ValueObject? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(ValueObject? left, ValueObject? right)
    {
        return !Equals(left, right);
    }
}