namespace BrawlhallaStat.Domain;

public class LegendStatistic
{
    public GamesStatistic Statistic { get; set; } = null!;

    public int LegendId { get; set; }
    public Legend Legend { get; set; } = null!;

    public int OwnerId { get; set; }
    public User Owner { get; set; } = null!;
}