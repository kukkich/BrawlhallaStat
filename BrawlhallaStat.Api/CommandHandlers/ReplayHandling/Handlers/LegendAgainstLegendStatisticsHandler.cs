namespace BrawlhallaStat.Api.CommandHandlers.ReplayHandling.Handlers;

public class LegendAgainstLegendStatisticsHandler : IReplayHandler
{
    public Task HandleAsync(ReplayHandlingContext context)
    {
        var opponentLegendIds = context.OpponentLegends
            .Select(l => l.Id);

        foreach (var statistic in
                 context.UserFromDb.LegendAgainstLegendStatistics
                     .Where(x => x.LegendId == context.Legend.Id)
                     .Where(x => opponentLegendIds.Contains(x.OpponentLegendId))
                     .Select(x => x.Statistic)
                )
        {
            context.WinLoseCounter.Count(statistic);
        }

        return Task.CompletedTask;
    }
}