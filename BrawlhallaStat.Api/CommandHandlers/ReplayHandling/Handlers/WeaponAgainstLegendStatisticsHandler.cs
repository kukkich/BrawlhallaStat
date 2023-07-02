namespace BrawlhallaStat.Api.CommandHandlers.ReplayHandling.Handlers;

public class WeaponAgainstLegendStatisticsHandler : IReplayHandler
{
    public Task HandleAsync(ReplayHandlingContext context)
    {
        var legend = context.Legend;
        var opponentLegendIds = context.OpponentLegends
            .Select(l => l.Id);

        foreach (var statistic in
                 context.UserFromDb.WeaponAgainstLegendStatistics
                     .Where(x => x.WeaponId == legend.FirstWeaponId ||
                                 x.WeaponId == legend.SecondWeaponId)
                     .Where(x => opponentLegendIds.Contains(x.OpponentLegendId))
                     .Select(x => x.Statistic)
                )
        {
            context.WinLoseCounter.Count(statistic);
        }

    }
}