using IntegrationTests.Common;
using Microsoft.Extensions.DependencyInjection;
using ViaEventAssociation.Core.Application.Common.CommandDispatcher;
using ViaEventAssociation.Core.Application.Features.Event.CreateEvent;
using VIAEventAssociation.Core.Domain.Common.Repositories;
using Xunit.Abstractions;

namespace IntegrationTests.Repositories.VeaEvent;

public class CreateVeaEventTests
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly ServiceProvider _serviceProvider;


    public CreateVeaEventTests(ITestOutputHelper output)
    {
        _serviceProvider = TestServiceProvider.CreateServiceProvider();

        _commandDispatcher = _serviceProvider.GetRequiredService<ICommandDispatcher>();
    }

    [Fact]
    public async Task CreateEventCommand_ShouldSucceedAndPersistEvent()
    {
        // Arrange
        var repo = _serviceProvider.GetRequiredService<IEventRepository>();
        var command = CreateEventCommand.Create().payload;

        // Act
        var result = await _commandDispatcher.DispatchAsync(command);
    }
}
