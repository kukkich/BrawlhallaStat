namespace BrawlhallaStat.Api.Replays.Exceptions;

public class InvalidReplaySizeException : ReplayUploadException
{
    public override string Message => "Invalid replay size";
}