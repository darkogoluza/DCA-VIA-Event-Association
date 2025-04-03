using Moq;
using ViaEventAssociation.Core.Application.Common.CommandDispatcher;
using ViaEventAssociation.Core.Application.Features.Event.UpdateTitle;
using VIAEventAssociation.Core.Domain.Common.Repositories;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Common.Dispatcher;

public class DispatcherSaveChangesTest
{
    [Fact]
    public async Task DispatchAsync_ShouldCallUnitOfWorkOnce()
    {
        // Arrange 
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockCommandDispatcher = new Mock<ICommandDispatcher>();
        var testCommand = UpdateEventTitleCommand.Create(Guid.NewGuid(), "Test");

        mockCommandDispatcher
            .Setup(d => d.DispatchAsync(testCommand))
            .ReturnsAsync(Result<None>.Success);

        var decorator = new CommandSaveChanges(mockCommandDispatcher.Object, mockUnitOfWork.Object);
        
        // Act
        await decorator.DispatchAsync(testCommand);

        // Assert
        mockCommandDispatcher.Verify(d => d.DispatchAsync(testCommand), Times.Once);
        mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
    }
}
