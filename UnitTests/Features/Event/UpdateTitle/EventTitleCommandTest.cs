using ViaEventAssociation.Core.Application.Features.Event.UpdateTitle;

namespace UnitTests.Features.Event.UpdateTitle;

public class EventTitleCommandTest
{
    [Fact]
    public void CreateCommand()
    {
        // Arrange
        string title = "New title";

        // Act
        var result = UpdateEventTitleCommand.Create(Guid.NewGuid(), title);

        // Assert
        Assert.True(result.isSuccess);
        Assert.Equal(title, result.payload.Title.Value);
    }

    [Fact]
    public void CreateCommand_Error()
    {
        // Arrange
        string title = "Ne";

        // Act
        var result = UpdateEventTitleCommand.Create(Guid.NewGuid(), title);

        // Assert
        Assert.True(result.isFailure);
    }
}
