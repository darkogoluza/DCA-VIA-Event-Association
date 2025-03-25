using ViaEventAssociation.Core.Application.Features.Event.ActivateEvent;

namespace UnitTests.Features.Event.ActivateEvent;

public class ActivateEventCommandTest
{
    [Fact]
    public void Activate()
    {
        // Arrange
        // None

        // Act
        var result = ActivateEventCommand.Create(Guid.NewGuid());

        // Assert
        Assert.True(result.isSuccess);
    }
}
