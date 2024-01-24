namespace BrawlhallaStat.Api.Replays.ReplayHandling;

public interface IReplayHandler
{
    public Task HandleAsync(ReplayHandlingContext context);
}