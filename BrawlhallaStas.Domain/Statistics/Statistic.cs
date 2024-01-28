namespace BrawlhallaStat.Domain.Statistics;

public class Statistic
{
    public int Wins { get; set; }
    public int Defeats { get; set; }
    public int Total => Wins + Defeats;
}