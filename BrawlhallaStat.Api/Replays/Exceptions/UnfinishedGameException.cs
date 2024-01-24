using BrawlhallaStat.Api.Exceptions;

namespace BrawlhallaStat.Api.Replays.Exceptions;

public class UnfinishedGameException : ApiException
{
    public override string Message => "The game wasn't over";
}