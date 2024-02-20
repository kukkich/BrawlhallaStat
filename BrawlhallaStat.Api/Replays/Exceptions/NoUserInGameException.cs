using System.Text;

namespace BrawlhallaStat.Api.Replays.Exceptions;

public class NoUserInGameException : ReplayUploadException
{
    public List<string> AvailableNickNames { get; }
    public readonly string ExpectedNickName;

    public override string Message => @$"
        Your nickname must match with your nickname in game.
        There is no player with the nickname {ExpectedNickName} in the game. Only {GatAvailableNickNamesMessage()} available.
        Change your nickname and retry";

    public NoUserInGameException(string expectedNickName, IEnumerable<string> availableNickNames)
    {
        AvailableNickNames = availableNickNames.ToList();
        ExpectedNickName = expectedNickName;
    }

    private string GatAvailableNickNamesMessage()
    {
        var builder = new StringBuilder();
        builder.Append("[");

        foreach (var nickName in AvailableNickNames.SkipLast(1))
        {
            builder.Append('\"');
            builder.Append(nickName);
            builder.Append('\"');
            builder.Append(',');
        }
        var lastNickName = AvailableNickNames[^1];
        builder.Append('\"');
        builder.Append(lastNickName);
        builder.Append('\"');
        builder.Append(']');

        return builder.ToString();
    }
}