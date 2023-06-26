using BrawlhallaStat.Domain.Base;

namespace BrawlhallaStat.Domain.Statistics;

public interface IConcreteStatistic : IHaveId<string>
{
    public string StatisticId { get; set; }
    public Statistic Statistic { get; set; }

    public string UserId { get; set; }
    public User User { get; set; }
}