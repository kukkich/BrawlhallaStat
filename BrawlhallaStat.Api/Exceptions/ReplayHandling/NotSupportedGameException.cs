namespace BrawlhallaStat.Api.Exceptions.ReplayHandling;

public class NotSupportedGameException : ApiException
{
    public NotSupportedGameException(string message)
        : base(message)
    {

    }
}