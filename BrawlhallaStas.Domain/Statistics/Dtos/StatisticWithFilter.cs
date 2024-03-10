namespace BrawlhallaStat.Domain.Statistics.Dtos;

public class StatisticWithFilter
{
    public Statistic Statistic { get; set; } = null!;
    public StatisticFilterDto Filter { get; set; } = null!;
}