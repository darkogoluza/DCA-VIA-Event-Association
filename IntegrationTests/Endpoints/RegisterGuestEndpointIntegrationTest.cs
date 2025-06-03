using System.Net;
using System.Net.Http.Json;
using IntegrationTests.Factories;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using ViaEventAssociation.Infrastructure.SqliteDmPersistence;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Guest;
using Xunit.Abstractions;

namespace IntegrationTests.Endpoints;

public class RegisterGuestEndpointIntegrationTest
{
    private readonly HttpClient _client;

    [Fact]
    public async Task RegisterGuestEndpoint_Should_RegisterGuestSuccessfully()
    {
        // Arrange
        await using WebApplicationFactory<Program> webApplicationFactory = new VeaWebApplicationFactory();
        HttpClient client = webApplicationFactory.CreateClient();

        var request = new RegisterGuestEndpointRequest(
            new RegisterGuestEndpointRequest.Body(
                FirstName: "John",
                LastName: "Doe",
                Email: "jhd@via.dk",
                ProfilePictureUrl: "https://example.com/john.jpg"
            )
        );

        // Act
        HttpResponseMessage responseMessage = await client.PostAsync(
            "/api/guest/register",
            JsonContent.Create(request)
        );

        IServiceScope serviceScope = webApplicationFactory.Services.CreateScope();
        var context = serviceScope.ServiceProvider.GetRequiredService<SqliteDmContext>();
        var guests = context.Guests.ToList();

        // Assert
        Assert.True(responseMessage.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);
        Assert.Single(guests);
    }

    [Fact]
    public async Task RegisterGuestEndpoint_Should_ReturnBadRequest_WhenEmailDomainIsInvalid()
    {
        // Arrange
        await using WebApplicationFactory<Program> webApplicationFactory = new VeaWebApplicationFactory();
        HttpClient client = webApplicationFactory.CreateClient();

        var request = new RegisterGuestEndpointRequest(
            new RegisterGuestEndpointRequest.Body(
                FirstName: "John",
                LastName: "Doe",
                Email: "jhd@via.com",
                ProfilePictureUrl: "https://example.com/john.jpg"
            )
        );

        // Act
        HttpResponseMessage responseMessage = await client.PostAsync(
            "/api/guest/register",
            JsonContent.Create(request)
        );

        IServiceScope serviceScope = webApplicationFactory.Services.CreateScope();
        var context = serviceScope.ServiceProvider.GetRequiredService<SqliteDmContext>();
        var guests = context.Guests.ToList();

        // Assert
        Assert.False(responseMessage.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, responseMessage.StatusCode);
        Assert.Empty(guests);
    }
}
