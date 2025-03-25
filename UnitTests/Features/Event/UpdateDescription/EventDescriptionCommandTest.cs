using ViaEventAssociation.Core.Application.Features.Event.UpdateDescription;

namespace UnitTests.Features.Event.UpdateDescription;

public class EventDescriptionCommandTest
{

    [Fact]
    public void UpdateDescriptionCommand()
    {
        // Arrange
        string description = "New description";

        // Act
        var result = UpdateEventDescriptionCommand.Create(Guid.NewGuid(), description);

        // Assert
        Assert.True(result.isSuccess);
        Assert.Equal(description, result.payload.Description.Value);
    }

    [Fact]
    public void UpdateDescriptionCommand_Error()
    {
        // Arrange
        string description = new string('A', 251);

        // Act
        var result = UpdateEventDescriptionCommand.Create(Guid.NewGuid(), description);

        // Assert
        Assert.True(result.isFailure);
    }
}
