namespace BrawlhallaStat.Api.Exceptions.ReplayHandling;

public class InvalidReplayFormatException : ApiException
{
    public override string Message => "Invalid replay format";
}