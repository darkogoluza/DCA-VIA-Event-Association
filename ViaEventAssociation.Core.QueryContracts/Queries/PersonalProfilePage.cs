using ViaEventAssociation.Core.QueryContracts.Contract;

namespace ViaEventAssociation.Core.QueryContracts.Queries;

public class PersonalProfilePage
{
    public record Query() : IQuery<Answer>;

    public record Answer();
}
