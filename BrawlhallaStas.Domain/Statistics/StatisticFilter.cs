using BrawlhallaStat.Domain.Base;
using BrawlhallaStat.Domain.Identity;

namespace BrawlhallaStat.Domain.Statistics;

public class StatisticFilter : StatisticFilterBase, IHaveId<string>
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;
}