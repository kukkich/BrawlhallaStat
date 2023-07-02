namespace BrawlhallaStat.Api.CommandHandlers.ReplayHandling.Handlers;

public class LegendAgainstWeaponStatisticsHandler : IReplayHandler
{
    public Task HandleAsync(ReplayHandlingContext context)
    {
        var opponentWeaponIds = context.OpponentWeapons.Select(x => x.Id);

        foreach (var statistic in
                 context.UserFromDb.LegendAgainstWeaponStatistics
                     .Where(x => x.LegendId == context.Legend.Id)
                     .Where(x => opponentWeaponIds.Contains(x.OpponentWeaponId))
                     .Select(x => x.Statistic)
                )
        {
            context.WinLoseCounter.Count(statistic);
        }

        return Task.CompletedTask;
    }
}