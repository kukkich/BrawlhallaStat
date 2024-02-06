namespace BrawlhallaStat.Api.Exceptions.Tokens;

public class TokenNotFoundException : ApiException
{
    public override string Message => "Token wasn't found";
}