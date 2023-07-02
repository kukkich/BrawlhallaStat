using BrawlhallaStat.Domain;

namespace BrawlhallaStat.Api.CommandHandlers.ReplayHandling.WinLoseCounting;

public interface IWinLoseCountStrategy
{
    public void Count(Statistic statistic);
}