namespace BrawlhallaStat.Domain.Statistics;

public interface IConcreteStatistic
{
    public string StatisticId { get; set; }
    public Statistic Statistic { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }
}