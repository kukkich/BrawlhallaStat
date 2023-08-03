namespace BrawlhallaStat.Api.Exceptions;

public class AlreadyExistException : ApiException
{
    public override string Message { get; }

    public AlreadyExistException(string message)
    {
        Message = message;
    }

    public AlreadyExistException(string who, string propertyName, string value)
    {
        Message = $"{who.ToLower()} with {propertyName.ToLower()} {value.ToLower()} already exists";
    }
}