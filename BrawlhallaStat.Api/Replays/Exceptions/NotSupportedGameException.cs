using BrawlhallaStat.Api.Exceptions;

namespace BrawlhallaStat.Api.Replays.Exceptions;

public class NotSupportedGameException : ApiException
{
    public NotSupportedGameException(string message)
        : base(message)
    {

    }
}