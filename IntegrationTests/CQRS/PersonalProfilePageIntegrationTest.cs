using IntegrationTests.Common;
using Microsoft.Extensions.DependencyInjection;
using ViaEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Core.QueryContracts.QueryDispatching;
using Xunit.Abstractions;

namespace IntegrationTests.CQRS;

public class PersonalProfilePageIntegrationTest
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly ServiceProvider _serviceProvider;

    public PersonalProfilePageIntegrationTest()
    {
        _serviceProvider = TestServiceProviderCQRS.CreateServiceProvider();

        _queryDispatcher = _serviceProvider.GetRequiredService<IQueryDispatcher>();
    }

    [Fact]
    public async Task Should_ReturnValidPersonalProfileData_ForValidUserId()
    {
        // Arrange
        var result = await _queryDispatcher.DispatchAsync(
            new PersonalProfilePage.Query("5893604a-5eff-46c4-8056-77161a6e9665"));


        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Guest);
        Assert.False(string.IsNullOrWhiteSpace(result.Guest.Name));
        Assert.False(string.IsNullOrWhiteSpace(result.Guest.Email));
        Assert.False(string.IsNullOrWhiteSpace(result.Guest.ProfilePictureUrl));
    
        Assert.True(result.UpcomingEventsCount >= 0);
        Assert.NotNull(result.UpcomingEvents);
        Assert.Equal(result.UpcomingEventsCount, result.UpcomingEvents.Count());

        Assert.NotNull(result.PastEvents);
        Assert.True(result.PendingInvitationsCount >= 0);
    }
}
