using VIAEventAssociation.Core.Domain.Aggregates.Guests.Values;
using VIAEventAssociation.Core.Domain.Common.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Features.Guest.RegisterGuest;

public class RegisterGuestCommand
{
    public VIAEventAssociation.Core.Domain.Aggregates.Guests.Entities.Guest Guest { get; private set; }

    public static Result<RegisterGuestCommand> Create(string firstName, string lastName, string email,
        string profilePictureUrl)
    {
        var firstNameResult = FirstName.Create(firstName);
        var lastNameResult = LastName.Create(lastName);
        var emailResult = Email.Create(email);
        Uri _profilePictureUrl = new Uri(profilePictureUrl);

        var registerResult = VIAEventAssociation.Core.Domain.Aggregates.Guests.Entities.Guest.Create(
            firstNameResult.payload, lastNameResult.payload, emailResult.payload,
            _profilePictureUrl);

        RegisterGuestCommand command = new RegisterGuestCommand(registerResult.payload);
        Result<RegisterGuestCommand> result = Result<RegisterGuestCommand>
            .FromResult(firstNameResult)
            .WithResult(lastNameResult)
            .WithResult(emailResult)
            .WithResult(registerResult);

        if (result.isSuccess)
        {
            result.payload = command;
        }

        return result;
    }

    public RegisterGuestCommand(VIAEventAssociation.Core.Domain.Aggregates.Guests.Entities.Guest guest)
    {
        Guest = guest;
    }
}
