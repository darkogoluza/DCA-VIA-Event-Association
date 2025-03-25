using ViaEventAssociation.Core.Application.Features.Event.CreateEvent;

namespace UnitTests.Features.Event.CreateEvent;

public class CreateEventCommandTest
{
    [Fact]
    public void CreateEventCommand_Success()
    {
        // Arrange

        // Act
        var result = CreateEventCommand.Create();

        // Assert
        Assert.True(result.isSuccess);
        Assert.NotEmpty(result.payload.VeaEvent.VeaEventId.Id.ToString());
    }
}
