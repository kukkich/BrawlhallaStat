namespace BrawlhallaStat.Api.Exceptions.Authentication;

public class InvalidPasswordException : ApiException
{
    public override string Message => "Invalid password";
}