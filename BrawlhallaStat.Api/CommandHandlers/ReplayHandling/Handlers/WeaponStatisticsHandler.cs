namespace BrawlhallaStat.Api.CommandHandlers.ReplayHandling.Handlers;

public class WeaponStatisticsHandler : IReplayHandler
{
    public Task HandleAsync(ReplayHandlingContext context)
    {
        var legend = context.Legend;
        foreach (var statistic in
                 context.UserFromDb.WeaponStatistics
                     .Where(x => x.WeaponId == legend.FirstWeaponId ||
                                 x.WeaponId == legend.SecondWeaponId)
                     .Select(x => x.Statistic)
                )
        {
            context.WinLoseCounter.Count(statistic);
        }

        return Task.CompletedTask;
    }
}