namespace BrawlhallaStat.Api.Replays.Exceptions;

public class NoUserInGameException : ReplayUploadException
{
    public readonly string ExpectedNickName;
    public override string Message => $"There is no player with the nickname {ExpectedNickName} in the game";

    public NoUserInGameException(string expectedNickName)
    {
        ExpectedNickName = expectedNickName;
    }
}