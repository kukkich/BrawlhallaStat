namespace BrawlhallaStat.Api.Exceptions;

public class AccessForbiddenException : ApiException 
{
    public AccessForbiddenException()
        : base("Insufficient rights to perform the operation")
    {
        
    }
}