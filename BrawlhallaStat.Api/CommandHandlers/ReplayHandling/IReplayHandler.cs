namespace BrawlhallaStat.Api.CommandHandlers.ReplayHandling;

public interface IReplayHandler
{
    public Task HandleAsync(ReplayHandlingContext context);
}