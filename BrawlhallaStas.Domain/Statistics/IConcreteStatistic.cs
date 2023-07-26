using BrawlhallaStat.Domain.Base;

namespace BrawlhallaStat.Domain.Statistics;

// TODO Make pair statistic 
// with properties like: entity, AgainstEntity
public interface IConcreteStatistic : IHaveId<string>
{
    public string StatisticId { get; set; }
    public Statistic Statistic { get; set; }

    public string UserId { get; set; }
    public User User { get; set; }
}