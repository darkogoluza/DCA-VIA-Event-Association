using VIAEventAssociation.Core.Domain.Aggregates.Events.Values;
using VIAEventAssociation.Core.Domain.Common.Bases;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;

public class Location : Entity
{
    private LocationName _locationName;
    private LocationCapacity _locationCapacity;
    private DateTime _startWorkingTimeDate;
    private DateTime _endWorkingTimeDate;

    private Location() // For EFC
    {
    }

    private Location(LocationName locationName, LocationCapacity locationCapacity, DateTime startWorkingTimeDate,
        DateTime endWorkingTimeDate)
    {
        _locationName = locationName;
        _locationCapacity = locationCapacity;
        _startWorkingTimeDate = startWorkingTimeDate;
        _endWorkingTimeDate = endWorkingTimeDate;
    }

    protected static Result<Location> Create(LocationName locationName, LocationCapacity locationCapacity,
        DateTime startWorkingTimeDate,
        DateTime endWorkingTimeDate)
    {
        throw new NotImplementedException();
    }

    protected Result<Location> UpdateName(LocationName name)
    {
        throw new NotImplementedException();
    }
}
