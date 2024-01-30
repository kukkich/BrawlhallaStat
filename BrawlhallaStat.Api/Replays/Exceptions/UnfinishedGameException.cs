namespace BrawlhallaStat.Api.Replays.Exceptions;

public class UnfinishedGameException : ReplayUploadException
{
    public override string Message => "The game wasn't over";
}