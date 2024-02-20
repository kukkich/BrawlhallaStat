namespace BrawlhallaStat.Api.Exceptions;

public abstract class ApiException : Exception
{
    protected ApiException() { }
    protected ApiException(string message)
        : base(message)
    { }
}