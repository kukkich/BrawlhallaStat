using BrawlhallaStat.Api.Exceptions;

namespace BrawlhallaStat.Api.Authentication.Exceptions.Tokens;

public class InvalidRefreshTokenException : ApiException
{
    public override string Message => "Invalid refresh token";
}