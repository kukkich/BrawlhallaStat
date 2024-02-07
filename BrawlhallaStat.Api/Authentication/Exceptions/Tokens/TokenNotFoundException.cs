using BrawlhallaStat.Api.Exceptions;

namespace BrawlhallaStat.Api.Authentication.Exceptions.Tokens;

public class TokenNotFoundException : ApiException
{
    public override string Message => "Token wasn't found";
}