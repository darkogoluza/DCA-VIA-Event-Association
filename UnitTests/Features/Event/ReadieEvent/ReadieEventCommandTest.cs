using ViaEventAssociation.Core.Application.Features.Event.ReadieEvent;

namespace UnitTests.Features.Event.ReadieEvent;

public class ReadieEventCommandTest
{
    [Fact]
    public void Redie()
    {
        // Arrange
        // None

        // Act
        var result = ReadieEventCommand.Create(Guid.NewGuid());

        // Assert
        Assert.True(result.isSuccess);
    }

    [Fact]
    public void Redie_WithMock()
    {
        // Arrange
        DateTime CurrentDateTimeMock() => new DateTime(2025, 3, 3, 12, 0, 0);

        // Act
        var result = ReadieEventCommand.Create(Guid.NewGuid(), CurrentDateTimeMock);

        // Assert
        Assert.True(result.isSuccess);
    }
}
