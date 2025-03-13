using VIAEventAssociation.Core.Domain.Aggregates.Guests.Entities;
using VIAEventAssociation.Core.Domain.Aggregates.Guests.Values;
using VIAEventAssociation.Core.Domain.Common.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.GuestTests;

public class RegisterGuestAggregateUnitTests
{
    [Fact]
    public void RegisterGuest()
    {
        // Arrange
        var firstNameResult = FirstName.Create("John");
        var lastNameResult = LastName.Create("Doe");
        var emailResult = Email.Create("jdo@via.dk");
        Uri profilePictureUrl =
            new Uri(
                "https://media.istockphoto.com/id/521573873/vector/unknown-person-silhouette-whith-blue-tie.jpg?s=2048x2048&w=is&k=20&c=cjOrS4d7gV46uXDx9iWH5n5uSEF6hhZ6Gebbp5j6USI=");

        // Act
        var registerResult = Guest.Create(firstNameResult.payload, lastNameResult.payload, emailResult.payload,
            profilePictureUrl);

        // Assert
        Assert.True(firstNameResult.isSuccess);
        Assert.True(lastNameResult.isSuccess);
        Assert.True(emailResult.isSuccess);
        Assert.True(registerResult.isSuccess);
        Assert.Equal(firstNameResult.payload, registerResult.payload._firstName);
        Assert.Equal(lastNameResult.payload, registerResult.payload._lastName);
        Assert.Equal(emailResult.payload, registerResult.payload._email);
        Assert.Equal(profilePictureUrl, registerResult.payload._profilePictureUrl);
        Assert.NotEmpty(registerResult.payload.GuestId.Id.ToString());
    }

    [Fact]
    public void RegisterGuest_EmailDomainIncorrect()
    {
        // Arrange

        // Act
        var emailResult = Email.Create("jdo@gmail.dk");

        // Assert
        Assert.True(emailResult.isFailure);
        Assert.Contains(Error.WrongEmailDomain(), emailResult.errors);
    }

    [Theory]
    [InlineData("wrongemail")]
    [InlineData("")]
    [InlineData("JDO@via.dk")]
    [InlineData("jdo@VIA.dk")]
    [InlineData("jdo@via.DK")]
    [InlineData("toolong@via.DK")]
    [InlineData("jd@via.DK")]
    public void RegisterGuest_EmailFormatIncorrect(string email)
    {
        // Arrange

        // Act
        var emailResult = Email.Create(email);

        // Assert
        Assert.True(emailResult.isFailure);
        Assert.Contains(Error.WrongEmailFormat(), emailResult.errors);
    }

    [Theory]
    [InlineData("")]
    [InlineData("A")]
    [InlineData("john")]
    [InlineData("JoHn")]
    [InlineData("John1")]
    [InlineData("John@")]
    [InlineData("John_")]
    [InlineData("Aaaaaaaaaaaaaaaaaaaaaaaaaa")]
    public void RegisterGuest_FirstNameInvalid(string firstName)
    {
        // Arrange

        // Act
        var firstNameResult = FirstName.Create(firstName);

        // Assert
        Assert.True(firstNameResult.isFailure);
        Assert.Contains(Error.WrongFirstNameFormat(), firstNameResult.errors);
    }

    [Theory]
    [InlineData("")]
    [InlineData("A")]
    [InlineData("doe")]
    [InlineData("DoE")]
    [InlineData("Doe1")]
    [InlineData("Doe@")]
    [InlineData("Doe_")]
    [InlineData("Aaaaaaaaaaaaaaaaaaaaaaaaaa")]
    public void RegisterGuest_LastNameInvalid(string lastName)
    {
        // Arrange

        // Act
        var lastNameResult = LastName.Create(lastName);

        // Assert
        Assert.True(lastNameResult.isFailure);
        Assert.Contains(Error.WrongLastNameFormat(), lastNameResult.errors);
    }

    [Theory]
    [InlineData("")]
    [InlineData("https://exa$mple.com")]
    [InlineData("/images/profile.jpg")]
    [InlineData("www.example.com/profile.jpg")]
    [InlineData("https://exa mple.com/profile.jpg")]
    [InlineData("https://example.com:abcd")]
    public void RegisterGuest_UrlInvalidFormat(string url)
    {
        // Assert
        Assert.Throws<UriFormatException>(() => new Uri(url));

    }
}
