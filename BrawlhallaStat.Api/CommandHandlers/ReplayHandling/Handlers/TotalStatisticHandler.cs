namespace BrawlhallaStat.Api.CommandHandlers.ReplayHandling.Handlers;

public class TotalStatisticHandler : IReplayHandler
{
    public Task HandleAsync(ReplayHandlingContext context)
    {
        context.WinLoseCounter.Count(context.UserFromDb.TotalStatistic);
        return Task.CompletedTask;
    }
}