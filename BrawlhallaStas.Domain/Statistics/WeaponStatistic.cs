namespace BrawlhallaStat.Domain.Statistics;

public class WeaponStatistic : IConcreteStatistic
{
    public string Id { get; set; } = null!;

    public int WeaponId { get; set; }
    public Weapon Weapon { get; set; } = null!;

    public string StatisticId { get; set; } = null!;
    public Statistic Statistic { get; set; } = null!;

    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;
}