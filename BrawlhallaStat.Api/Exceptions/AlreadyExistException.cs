namespace BrawlhallaStat.Api.Exceptions;

public class AlreadyExistException : ApiException
{
    public override string Message { get; }

    public AlreadyExistException(string message)
    {
        Message = message;
    }
}