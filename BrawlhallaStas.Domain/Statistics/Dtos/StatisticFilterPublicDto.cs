using BrawlhallaStat.Domain.Base;

namespace BrawlhallaStat.Domain.Statistics.Dtos;

public class StatisticFilterPublicDto : StatisticFilterBase, IHaveId<string>
{
    public string Id { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}
