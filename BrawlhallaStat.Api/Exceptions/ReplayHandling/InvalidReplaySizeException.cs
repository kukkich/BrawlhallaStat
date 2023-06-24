namespace BrawlhallaStat.Api.Exceptions.ReplayHandling;

public class InvalidReplaySizeException : ApiException
{
    public override string Message => "Invalid replay size";
}