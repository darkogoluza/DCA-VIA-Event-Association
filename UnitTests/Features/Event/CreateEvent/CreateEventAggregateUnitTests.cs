using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Values;
using VIAEventAssociation.Core.Domain.Common.Values;
using Xunit.Abstractions;

namespace UnitTests.Features.Event.CreateEvent;

public class CreateEventAggregateUnitTests
{
    [Fact]
    public void CreateANewEvent_EventId()
    {
        // Arrange
        // None

        // Act
        var result = VeaEvent.Create();

        // Assert
        Assert.True(result.isSuccess);
        var veaEvent = result.payload;
        Assert.NotEmpty(veaEvent.VeaEventId.Id.ToString());
    }
}
