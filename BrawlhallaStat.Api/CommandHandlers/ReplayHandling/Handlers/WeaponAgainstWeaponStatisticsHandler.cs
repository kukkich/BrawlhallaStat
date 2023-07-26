namespace BrawlhallaStat.Api.CommandHandlers.ReplayHandling.Handlers;

public class WeaponAgainstWeaponStatisticsHandler : IReplayHandler
{
    public Task HandleAsync(ReplayHandlingContext context)
    {
        var opponentWeaponIds = context.OpponentWeapons.Select(x => x.Id);
        var legend = context.Legend;

        foreach (var statistic in
                 context.UserFromDb.WeaponAgainstWeaponStatistics
                     .Where(x => x.WeaponId == legend.FirstWeaponId ||
                                 x.WeaponId == legend.SecondWeaponId)
                     .Where(x => opponentWeaponIds.Contains(x.OpponentWeaponId))
                     .Select(x => x.Statistic)
                )
        {
            context.WinLoseCounter.Count(statistic);
        }

        return Task.CompletedTask;
    }
}