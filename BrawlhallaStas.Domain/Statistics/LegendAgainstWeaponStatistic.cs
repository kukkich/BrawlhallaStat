namespace BrawlhallaStat.Domain.Statistics;

public class LegendAgainstWeaponStatistic : IConcreteStatistic
{
    public int LegendId { get; set; }
    public Legend Legend { get; set; } = null!;

    public int OpponentWeaponId { get; set; }
    public Weapon OpponentWeapon { get; set; } = null!;

    public string StatisticId { get; set; } = null!;
    public Statistic Statistic { get; set; } = null!;   

    public int UserId { get; set; }
    public User User { get; set; } = null!;
}