namespace BrawlhallaStat.Domain.Statistics.Dtos;

public class StatisticWithFilter
{
    public Statistic Statistic { get; set; } = null!;
    public StatisticFilterPublicDto Filter { get; set; } = null!;
}