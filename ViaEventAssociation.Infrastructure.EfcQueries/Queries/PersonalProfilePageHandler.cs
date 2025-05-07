using ViaEventAssociation.Core.QueryContracts.Contract;
using ViaEventAssociation.Core.QueryContracts.Queries;

namespace ViaEventAssociation.Infrastructure.EfcQueries.Queries;

public class PersonalProfilePageHandler(VeadatabaseProductionContext context): IQueryHandler<PersonalProfilePage.Query, PersonalProfilePage.Answer>
{
    public Task<PersonalProfilePage.Answer> HandleAsync(PersonalProfilePage.Query query)
    {
        throw new NotImplementedException();
    }
}
