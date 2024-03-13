namespace BrawlhallaStat.Domain.Statistics;

public class StatisticWithFilter
{
    public Statistic Statistic { get; set; } = null!;
    public StatisticFilter Filter { get; set; } = null!;
}