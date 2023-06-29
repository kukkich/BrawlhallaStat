namespace BrawlhallaStat.Api.Exceptions.ReplayHandling;

public class NoUserInGameException : ApiException
{
    private readonly string _expectedNickName;
    public override string Message => $"There is no player with the nickname {_expectedNickName} in the game";

    public NoUserInGameException(string expectedNickName)
    {
        _expectedNickName = expectedNickName;
    }
}