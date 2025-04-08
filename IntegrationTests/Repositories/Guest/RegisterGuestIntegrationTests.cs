using IntegrationTests.Common;
using Microsoft.Extensions.DependencyInjection;
using ViaEventAssociation.Core.Application.Common.CommandDispatcher;
using ViaEventAssociation.Core.Application.Features.Guest.RegisterGuest;
using VIAEventAssociation.Core.Domain.Common.Repositories;
using ViaEventAssociation.Infrastructure.SqliteDmPersistence;

namespace IntegrationTests.Repositories.Guest;

public class RegisterGuestIntegrationTests : IDisposable
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly ServiceProvider _serviceProvider;

    public RegisterGuestIntegrationTests()
    {
        _serviceProvider = TestServiceProvider.CreateServiceProvider();

        _commandDispatcher = _serviceProvider.GetRequiredService<ICommandDispatcher>();
    }

    public void Dispose()
    {
        _serviceProvider.GetRequiredService<SqliteDmContext>().ChangeTracker.Clear();
    }

    [Fact]
    public async Task RegisterGuest_ShouldSucceed_AndPersistGuestInRepository()
    {
        // Arrange
        var repo = _serviceProvider.GetRequiredService<IGuestRepository>();

        string firstName = "John";
        string lastName = "Doe";
        string email = "jhd@via.dk";
        string url =
            "https://media.istockphoto.com/id/521573873/vector/unknown-person-silhouette-whith-blue-tie.jpg?s=2048x2048&w=is&k=20&c=cjOrS4d7gV46uXDx9iWH5n5uSEF6hhZ6Gebbp5j6USI=";

        var commandRegister = RegisterGuestCommand.Create(firstName, lastName, email, url)
            .payload;

        // Act
        var result = await _commandDispatcher.DispatchAsync(commandRegister);

        // Arrange
        Assert.True(result.isSuccess);
        Assert.Single(await repo.GetAllAsync());
        Assert.Equal(commandRegister.Guest, await repo.GetAsync(commandRegister.Guest.GuestId.Id));
    }
}
