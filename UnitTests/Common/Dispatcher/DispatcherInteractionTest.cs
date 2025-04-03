using Moq;
using Microsoft.Extensions.DependencyInjection;
using ViaEventAssociation.Core.Application.Common.CommandDispatcher;
using ViaEventAssociation.Core.Application.Common.CommandHandler;
using ViaEventAssociation.Core.Application.Features.Event.UpdateTitle;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Common.Dispatcher;

public class DispatcherInteractionTest
{
    [Fact]
    public async Task DispatchAsync_ShouldCallCommandHandlerOnce()
    {
        // Arrange
        var mockHandler = new Mock<ICommandHandler<UpdateEventTitleCommand>>();
        mockHandler
            .Setup(handler => handler.HandleAsync(It.IsAny<UpdateEventTitleCommand>()))
            .ReturnsAsync(Result<None>.Success(None.Value));

        var serviceProvider = new ServiceCollection()
            .AddSingleton(mockHandler.Object)
            .BuildServiceProvider();

        var dispatcher = new CommandDispatcher(serviceProvider);
        var command = UpdateEventTitleCommand.Create(Guid.NewGuid(), "Test").payload;

        // Act
        await dispatcher.DispatchAsync(command);

        // Assert
        mockHandler.Verify(handler => handler.HandleAsync(command), Times.Once);
    }
}
