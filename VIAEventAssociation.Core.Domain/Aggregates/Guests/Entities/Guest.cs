﻿using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;
using VIAEventAssociation.Core.Domain.Aggregates.Guests.Values;
using VIAEventAssociation.Core.Domain.Common.Bases;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace VIAEventAssociation.Core.Domain.Aggregates.Guests.Entities;

public class Guest : AggregateRoot
{
    private FirstName _firstName;
    private LastName _lastName;
    private MiddleName? _middleName;
    private Email _email;

    private Guest(FirstName firstName, LastName lastName, Email email, MiddleName? middleName = null)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _email = email;
    }

    public static Result<Guest> Create(string firstName, string lastName, string email, string? middleName = null)
    {
        throw new NotImplementedException();
    }

    public static Result<Guest> Register(Guest guest)
    {
        throw new NotImplementedException();
    }

    public Result<RequestToJoin> RequestToJoin(Guid id)
    {
        throw new NotImplementedException();
    }

    public Result<None> JoinEvent(Guid id)
    {
        throw new NotImplementedException();
    }

    public Result<None> LeaveEvent(Guid id)
    {
        throw new NotImplementedException();
    }
}
