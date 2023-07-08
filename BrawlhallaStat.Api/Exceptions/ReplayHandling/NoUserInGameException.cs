namespace BrawlhallaStat.Api.Exceptions.ReplayHandling;

public class NoUserInGameException : ApiException
{
    public readonly string ExpectedNickName;
    public readonly string? ButWas;
    public override string Message => $"There is no player with the nickname {ExpectedNickName} in the game";

    public NoUserInGameException(string expectedNickName, string? butWas=null)
    {
        ExpectedNickName = expectedNickName;
        ButWas = butWas;
    }
}