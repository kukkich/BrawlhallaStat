namespace BrawlhallaStat.Api.CommandHandlers.ReplayHandling.Handlers;

public class LegendStatisticsHandler : IReplayHandler
{
    public Task HandleAsync(ReplayHandlingContext context)
    {
        var statistic = context.UserFromDb.LegendStatistics
            .First(x => x.LegendId == context.Legend.Id)
            .Statistic;

        context.WinLoseCounter.Count(statistic);

        return Task.CompletedTask;
    }
}