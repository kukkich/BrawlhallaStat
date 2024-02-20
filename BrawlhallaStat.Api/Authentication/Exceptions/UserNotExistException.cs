using BrawlhallaStat.Api.Exceptions;

namespace BrawlhallaStat.Api.Authentication.Exceptions;

public class UserNotExistException : ApiException
{
    private readonly string _login;
    public override string Message => $"User with login {_login} doesn't exist";

    public UserNotExistException(string login)
    {
        _login = login;
    }
}