using BrawlhallaStat.Domain;

namespace BrawlhallaStat.Api.CommandHandlers.ReplayHandling.WinLoseCounting;

public class LoseCounter : IWinLoseCountStrategy
{
    public void Count(Statistic statistic)
    {
        statistic.Lost++;
    }
}