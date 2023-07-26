using BrawlhallaStat.Domain;

namespace BrawlhallaStat.Api.CommandHandlers.ReplayHandling.WinLoseCounting;

public class WinCounter : IWinLoseCountStrategy
{
    public void Count(Statistic statistic)
    {
        statistic.Wins++;
    }
}