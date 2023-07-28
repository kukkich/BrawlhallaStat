namespace BrawlhallaStat.Api.Exceptions.ReplayHandling;

public class UnfinishedGameException : ApiException
{
    public override string Message => "The game wasn't over. It was probably abandoned";
}