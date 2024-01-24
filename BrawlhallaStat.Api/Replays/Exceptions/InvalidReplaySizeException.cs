using BrawlhallaStat.Api.Exceptions;

namespace BrawlhallaStat.Api.Replays.Exceptions;

public class InvalidReplaySizeException : ApiException
{
    public override string Message => "Invalid replay size";
}