using BrawlhallaStat.Api.Exceptions;

namespace BrawlhallaStat.Api.Replays.Exceptions;

public class NoUserInGameException : ApiException
{
    public readonly string ExpectedNickName;
    public override string Message => $"There is no player with the nickname {ExpectedNickName} in the game";

    public NoUserInGameException(string expectedNickName)
    {
        ExpectedNickName = expectedNickName;
    }
}