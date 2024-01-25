namespace BrawlhallaStat.Api.Replays.Exceptions;

public class InvalidReplayFormatException : ReplayUploadException
{
    public override string Message => "Invalid replay format";
}