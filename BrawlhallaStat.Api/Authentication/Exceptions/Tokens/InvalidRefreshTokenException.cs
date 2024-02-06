namespace BrawlhallaStat.Api.Exceptions.Tokens;

public class InvalidRefreshTokenException : ApiException
{
    public override string Message => "Invalid refresh token";
}