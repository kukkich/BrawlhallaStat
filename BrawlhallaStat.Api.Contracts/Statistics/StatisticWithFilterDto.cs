using BrawlhallaStat.Domain.Statistics;

namespace BrawlhallaStat.Api.Contracts.Statistics;

public class StatisticWithFilterDto
{
    public Statistic Statistic { get; set; } = null!;
    public StatisticFilterPublicDto Filter { get; set; } = null!;
}