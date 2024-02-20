using BrawlhallaStat.Api.Exceptions;

namespace BrawlhallaStat.Api.Authentication.Exceptions;

public class InvalidPasswordException : ApiException
{
    public override string Message => "Invalid password";
}