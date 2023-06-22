namespace BrawlhallaStat.Domain;

public class WeaponStatistic
{
    public GamesStatistic Statistic { get; set; } = null!;

    public int WeaponId { get; set; }
    public Weapon Weapon { get; set; } = null!;

    public int OwnerId { get; set; }
    public User Owner { get; set; } = null!;
}