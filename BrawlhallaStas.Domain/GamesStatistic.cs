namespace BrawlhallaStat.Domain;

public class GamesStatistic
{
    public int Wins { get; set; }
    public int Lost { get; set; }
    public int Total => Wins + Lost;
}