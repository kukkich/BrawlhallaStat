namespace BrawlhallaStat.Domain.Statistics.Dtos;

public class StatisticWithFilterDto
{
    public Statistic Statistic { get; set; } = null!;
    public StatisticFilterPublicDto Filter { get; set; } = null!;
}