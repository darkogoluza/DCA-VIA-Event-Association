namespace ViaEventAssociation.Core.QueryContracts.Exceptions;

public class QueryHandlerNotFoundException : Exception
{
    public QueryHandlerNotFoundException(string queryType, string answerType)
        : base($"No query handler found for query type '{queryType}' and answer type '{answerType}'.")
    {
    }
}
