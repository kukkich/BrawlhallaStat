namespace BrawlhallaStat.Api.Replays.Exceptions;

public class NotSupportedGameException : ReplayUploadException
{
    public NotSupportedGameException(string message)
        : base(message)
    { }
}