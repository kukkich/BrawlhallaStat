using BrawlhallaStat.Api.Exceptions;

namespace BrawlhallaStat.Api.Replays.Exceptions;

public class InvalidReplayFormatException : ApiException
{
    public override string Message => "Invalid replay format";
}