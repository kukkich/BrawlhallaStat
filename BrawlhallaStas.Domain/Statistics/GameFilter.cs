using BrawlhallaStat.Domain.GameEntities.Views;

namespace BrawlhallaStat.Domain.Statistics;

public class GameFilter
{
    public StatisticFilter Filter { get; set; } = null!;
    public GameStatisticView Game { get; set; } = null!;
}